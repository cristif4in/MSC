using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Runs automatically when Play mode starts, no matter which scene objects are
/// enabled. It gives the exported game a normal sky, a working sun, brighter
/// ambient light, turns off the exported fog and the huge cloud/rain sheets,
/// then writes MSCFix.log and a few screenshots into the project folder so the
/// result can be inspected.
/// </summary>
public static class MSCWorldFixBootstrap
{
	[RuntimeInitializeOnLoadMethod]
	private static void Init()
	{
		GameObject gameObject = new GameObject("MSC World Fix");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent<MSCWorldFixRunner>();
	}
}

public class MSCWorldFixRunner : MonoBehaviour
{
	private static readonly List<string> _lines = new List<string>();

	private static string ProjectRoot
	{
		get
		{
			return Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace('\\', '/');
		}
	}

	private static string LogPath
	{
		get
		{
			return ProjectRoot + "/MSCFix.log";
		}
	}

	private IEnumerator Start()
	{
		Log("=== MSC World Fix " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ===");
		Log("unity=" + Application.unityVersion + " scene=" + Application.loadedLevelName);

		string lastScene = Application.loadedLevelName;
		int screenshot = 1;
		int tick = 0;

		while (tick <= 60)
		{
			if (Application.loadedLevelName != lastScene)
			{
				lastScene = Application.loadedLevelName;
				Log("scene changed to " + lastScene);
				ApplyWorldFix(true);
				yield return new WaitForSeconds(1f);
				DumpDiagnostics();
				TakeScreenshot("MSC_screenshot_" + screenshot + ".png");
				screenshot++;
			}

			ApplyWorldFix(tick < 4 || tick % 20 == 0);

			if (tick == 2 || tick == 5 || tick == 9 || tick == 14)
			{
				DumpDiagnostics();
			}

			if (tick == 1 || tick == 3 || tick == 6 || tick == 10 || tick == 15)
			{
				TakeScreenshot("MSC_screenshot_" + screenshot + ".png");
				screenshot++;
			}

			tick++;
			yield return new WaitForSeconds(1f);
		}

		Flush();
	}

	private static void ApplyWorldFix(bool fullScan)
	{
		// Fog from the export fades the scene to a dark color.
		RenderSettings.fog = false;

		// Bright, normal outdoor ambient light.
		RenderSettings.ambientMode = AmbientMode.Trilight;
		RenderSettings.ambientSkyColor = new Color(0.62f, 0.7f, 0.82f, 1f);
		RenderSettings.ambientEquatorColor = new Color(0.55f, 0.55f, 0.55f, 1f);
		RenderSettings.ambientGroundColor = new Color(0.34f, 0.3f, 0.26f, 1f);
		RenderSettings.ambientIntensity = 1.1f;

		// Make sure there is one strong directional sun.
		Light sun = FindBestSun();
		if (sun == null)
		{
			GameObject sunObject = new GameObject("MSC Sun");
			sun = sunObject.AddComponent<Light>();
			sun.type = LightType.Directional;
			sun.transform.rotation = Quaternion.Euler(50f, -35f, 0f);
		}
		sun.gameObject.SetActive(true);
		sun.enabled = true;
		sun.type = LightType.Directional;
		sun.intensity = Mathf.Max(1.3f, sun.intensity);
		sun.color = new Color(1f, 0.97f, 0.9f, 1f);
		sun.shadows = LightShadows.Soft;
		RenderSettings.sun = sun;

		// Replace the exported skybox with a simple, reliable blue sky.
		Shader skyShader = Shader.Find("Skybox/Procedural");
		if (skyShader != null && (RenderSettings.skybox == null || RenderSettings.skybox.name != "MSC Runtime Sky"))
		{
			Material sky = new Material(skyShader);
			sky.name = "MSC Runtime Sky";
			sky.SetColor("_SkyTint", new Color(0.32f, 0.55f, 0.9f, 1f));
			sky.SetColor("_GroundColor", new Color(0.55f, 0.5f, 0.42f, 1f));
			sky.SetFloat("_Exposure", 1.25f);
			sky.SetFloat("_AtmosphereThickness", 0.9f);
			sky.SetFloat("_SunSize", 0.04f);
			RenderSettings.skybox = sky;
			DynamicGI.UpdateEnvironment();
		}

		int imageEffectsOff = 0;
		int cloudsOff = 0;
		int rainOff = 0;

		Camera[] cameras = UnityEngine.Object.FindObjectsOfType<Camera>();
		foreach (Camera camera in cameras)
		{
			if (camera == null)
			{
				continue;
			}

			// The exported screen-space image effects could darken everything.
			MonoBehaviour[] behaviours = camera.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour behaviour in behaviours)
			{
				if (behaviour == null)
				{
					continue;
				}
				string ns = behaviour.GetType().Namespace;
				if (ns != null && ns.StartsWith("UnityStandardAssets.ImageEffects"))
				{
					behaviour.enabled = false;
					imageEffectsOff++;
				}
			}

			// Full screen camera that clears to a flat (often black) color.
			if (camera.targetTexture == null && !camera.orthographic && camera.depth >= 4f && camera.clearFlags == CameraClearFlags.SolidColor)
			{
				if (RenderSettings.skybox != null)
				{
					camera.clearFlags = CameraClearFlags.Skybox;
				}
				else
				{
					camera.backgroundColor = new Color(0.4f, 0.62f, 0.9f, 1f);
				}
			}
		}

		if (fullScan)
		{
			Renderer[] renderers = UnityEngine.Object.FindObjectsOfType<Renderer>();
			foreach (Renderer renderer in renderers)
			{
				if (renderer == null || renderer.gameObject == null)
				{
					continue;
				}

				string objectName = renderer.gameObject.name.ToLowerInvariant();
				Material[] materials = renderer.sharedMaterials;
				bool cloud = false;
				bool rain = false;

				for (int i = 0; i < materials.Length; i++)
				{
					Material material = materials[i];
					if (material == null)
					{
						continue;
					}

					string materialName = material.name.ToLowerInvariant();
					if (materialName.Contains("cloud") && !materialName.Contains("particle"))
					{
						cloud = true;
					}
					if (materialName.Contains("rainarea") || materialName.Contains("rain_area") || materialName.Contains("rainplayer"))
					{
						rain = true;
					}
				}

				if (objectName.Contains("cloud"))
				{
					cloud = true;
				}
				if (objectName.Contains("rainarea") || objectName.Contains("rain_area") || objectName.Contains("rainplayer"))
				{
					rain = true;
				}

				// The exported cloud layers are enormous planes that can cover the
				// whole view. The rain sheets look the same when it is not raining.
				if (cloud)
				{
					if (renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}
					cloudsOff++;
				}
				else if (rain)
				{
					if (renderer.gameObject.activeSelf)
					{
						renderer.gameObject.SetActive(false);
					}
					rainOff++;
				}
			}
		}

		Log("apply(" + fullScan + "): sun=" + ((sun != null) ? (sun.name + " intensity=" + sun.intensity) : "NULL")
			+ " sky=" + ((RenderSettings.skybox != null) ? RenderSettings.skybox.name : "NULL")
			+ " fog=" + RenderSettings.fog
			+ " imageEffectsOff=" + imageEffectsOff
			+ " cloudsOff=" + cloudsOff
			+ " rainOff=" + rainOff);
	}

	private static Light FindBestSun()
	{
		Light best = null;
		float bestIntensity = -1f;
		Light[] lights = UnityEngine.Object.FindObjectsOfType<Light>();
		for (int i = 0; i < lights.Length; i++)
		{
			Light light = lights[i];
			if (light == null || light.type != LightType.Directional)
			{
				continue;
			}
			if (!light.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (light.intensity > bestIntensity)
			{
				bestIntensity = light.intensity;
				best = light;
			}
		}
		return best;
	}

	private static void DumpDiagnostics()
	{
		Log("--- diagnostics ---");
		Log("skybox=" + ((RenderSettings.skybox != null) ? (RenderSettings.skybox.name + " shader=" + ((RenderSettings.skybox.shader != null) ? RenderSettings.skybox.shader.name : "NULL")) : "NULL"));
		Log("sun=" + ((RenderSettings.sun != null) ? (RenderSettings.sun.name + " intensity=" + RenderSettings.sun.intensity) : "NULL"));
		Log("fog=" + RenderSettings.fog + " fogColor=" + RenderSettings.fogColor + " ambientMode=" + RenderSettings.ambientMode + " ambient=" + RenderSettings.ambientSkyColor);

		Camera main = FindMainCamera();
		if (main == null)
		{
			Log("no screen camera found");
			return;
		}

		Log("main camera '" + main.name + "' pos=" + main.transform.position + " rot=" + main.transform.eulerAngles
			+ " clear=" + main.clearFlags + " depth=" + main.depth + " mask=" + main.cullingMask
			+ " fov=" + main.fieldOfView + " near=" + main.nearClipPlane + " far=" + main.farClipPlane);

		Camera[] cameras = UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < cameras.Length; i++)
		{
			Camera camera = cameras[i];
			if (camera == null || camera.targetTexture != null || !camera.gameObject.activeInHierarchy)
			{
				continue;
			}
			Log("screen camera '" + camera.name + "' enabled=" + camera.enabled + " clear=" + camera.clearFlags
				+ " depth=" + camera.depth + " mask=" + camera.cullingMask + " pos=" + camera.transform.position);
		}

		// Collider hits straight ahead.
		Vector3[] points = new Vector3[5];
		points[0] = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
		points[1] = new Vector3(Screen.width * 0.5f, Screen.height * 0.85f, 0f);
		points[2] = new Vector3(Screen.width * 0.5f, Screen.height * 0.15f, 0f);
		points[3] = new Vector3(Screen.width * 0.15f, Screen.height * 0.5f, 0f);
		points[4] = new Vector3(Screen.width * 0.85f, Screen.height * 0.5f, 0f);

		for (int p = 0; p < points.Length; p++)
		{
			Ray ray = main.ScreenPointToRay(points[p]);
			RaycastHit[] hits = Physics.RaycastAll(ray, 5000f);
			if (hits.Length == 0)
			{
				Log("ray " + p + " at " + points[p] + ": no collider hit");
				continue;
			}
			Array.Sort(hits, delegate(RaycastHit a, RaycastHit b) { return a.distance.CompareTo(b.distance); });
			int max = Mathf.Min(3, hits.Length);
			for (int h = 0; h < max; h++)
			{
				Collider collider = hits[h].collider;
				Renderer renderer = (collider != null) ? collider.GetComponent<Renderer>() : null;
				Log("ray " + p + " hit " + h + ": '" + ((collider != null) ? collider.gameObject.name : "?")
					+ "' layer=" + ((collider != null) ? collider.gameObject.layer : -1)
					+ " dist=" + hits[h].distance.ToString("F2")
					+ " point=" + hits[h].point
					+ " materials=" + DescribeMaterials(renderer));
			}
		}

		// Renderers crossing the view ray, even without colliders.
		Ray centerRay = main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
		List<Renderer> near = new List<Renderer>();
		List<float> distances = new List<float>();
		Renderer[] all = UnityEngine.Object.FindObjectsOfType<Renderer>();
		for (int i = 0; i < all.Length; i++)
		{
			Renderer renderer = all[i];
			if (renderer == null || !renderer.enabled || !renderer.gameObject.activeInHierarchy)
			{
				continue;
			}
			float distance;
			if (renderer.bounds.IntersectRay(centerRay, out distance) && distance < 500f)
			{
				near.Add(renderer);
				distances.Add(distance);
			}
		}

		for (int i = 0; i < near.Count; i++)
		{
			for (int j = i + 1; j < near.Count; j++)
			{
				if (distances[j] < distances[i])
				{
					float tmp = distances[i];
					distances[i] = distances[j];
					distances[j] = tmp;
					Renderer tmpRenderer = near[i];
					near[i] = near[j];
					near[j] = tmpRenderer;
				}
			}
		}

		int count = Mathf.Min(12, near.Count);
		Log("renderers crossing the view center ray: " + near.Count);
		for (int i = 0; i < count; i++)
		{
			Renderer renderer = near[i];
			Log("  " + i + " '" + renderer.gameObject.name + "' dist=" + distances[i].ToString("F2")
				+ " size=" + renderer.bounds.size + " layer=" + renderer.gameObject.layer
				+ " materials=" + DescribeMaterials(renderer));
		}
	}

	private static Camera FindMainCamera()
	{
		Camera best = null;
		Camera[] cameras = UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < cameras.Length; i++)
		{
			Camera camera = cameras[i];
			if (camera == null || !camera.enabled || camera.targetTexture != null || !camera.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (best == null || camera.depth > best.depth)
			{
				best = camera;
			}
		}
		return best;
	}

	private static string DescribeMaterials(Renderer renderer)
	{
		if (renderer == null)
		{
			return "-";
		}
		Material[] materials = renderer.sharedMaterials;
		StringBuilder builder = new StringBuilder();
		for (int i = 0; i < materials.Length; i++)
		{
			Material material = materials[i];
			if (material == null)
			{
				builder.Append("[null]");
				continue;
			}
			builder.Append("[");
			builder.Append(material.name);
			builder.Append(" shader=");
			builder.Append((material.shader != null) ? material.shader.name : "NULL");
			builder.Append(" color=");
			if (material.HasProperty("_Color"))
			{
				builder.Append(material.GetColor("_Color"));
			}
			else if (material.HasProperty("_Tint"))
			{
				builder.Append(material.GetColor("_Tint"));
			}
			else
			{
				builder.Append("-");
			}
			builder.Append(" mainTex=");
			if (material.HasProperty("_MainTex"))
			{
				Texture texture = material.GetTexture("_MainTex");
				builder.Append((texture != null) ? texture.name : "NULL");
			}
			else
			{
				builder.Append("-");
			}
			builder.Append("]");
		}
		return builder.ToString();
	}

	private static void TakeScreenshot(string fileName)
	{
		string path = ProjectRoot + "/" + fileName;
		try
		{
			Application.CaptureScreenshot(path);
			Log("screenshot requested: " + path);
		}
		catch (Exception exception)
		{
			Log("screenshot failed: " + exception.Message);
		}
	}

	private static void Log(string message)
	{
		_lines.Add(message);
		Debug.Log("[MSC FIX] " + message);
		Flush();
	}

	private static void Flush()
	{
		try
		{
			File.WriteAllLines(LogPath, _lines.ToArray());
		}
		catch (Exception exception)
		{
			Debug.Log("[MSC FIX] could not write log: " + exception.Message);
		}
	}
}
