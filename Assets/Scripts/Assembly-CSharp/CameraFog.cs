using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraFog : MonoBehaviour
{
	public bool Enabled;

	public float StartDistance;

	public float EndDistance;

	public FogMode Mode;

	public float Density;

	public Color Color;

	private float _startDistance;

	private float _endDistance;

	private FogMode _mode;

	private float _density;

	private Color _color;

	private bool _enabled;

	private bool _loggedCamera;

	private void OnPreRender()
	{
		Camera cam = GetComponent<Camera>();
		if (cam != null && cam.name == "Camera")
		{
			if (!_loggedCamera)
			{
				_loggedCamera = true;
				Debug.Log("[MSC FIX] " + cam.name + " clear=" + cam.clearFlags + " skybox=" + ((RenderSettings.skybox != null) ? RenderSettings.skybox.name : "NULL"));
			}
			cam.clearFlags = CameraClearFlags.SolidColor;
			cam.backgroundColor = new Color(0.19215687f, 0.3019608f, 0.4745098f, 1f);
		}
		_startDistance = RenderSettings.fogStartDistance;
		_endDistance = RenderSettings.fogEndDistance;
		_mode = RenderSettings.fogMode;
		_density = RenderSettings.fogDensity;
		_color = RenderSettings.fogColor;
		_enabled = RenderSettings.fog;
		RenderSettings.fog = Enabled;
		RenderSettings.fogStartDistance = StartDistance;
		RenderSettings.fogEndDistance = EndDistance;
		RenderSettings.fogMode = Mode;
		RenderSettings.fogDensity = Density;
		RenderSettings.fogColor = Color;
	}

	private void OnPostRender()
	{
		RenderSettings.fog = _enabled;
		RenderSettings.fogStartDistance = _startDistance;
		RenderSettings.fogEndDistance = _endDistance;
		RenderSettings.fogMode = _mode;
		RenderSettings.fogDensity = _density;
		RenderSettings.fogColor = _color;
	}
}
