using UnityEngine;

[RequireComponent(typeof(Camera))]
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

	private void OnPreRender()
	{
		if (!Application.isPlaying)
		{
			return;
		}

		_startDistance = RenderSettings.fogStartDistance;
		_endDistance = RenderSettings.fogEndDistance;
		_mode = RenderSettings.fogMode;
		_density = RenderSettings.fogDensity;
		_color = RenderSettings.fogColor;
		_enabled = RenderSettings.fog;

		// The exported fog settings fade the world to a dark color, which looks
		// like a mostly black screen. Keep fog off while the camera renders.
		RenderSettings.fog = false;
	}

	private void OnPostRender()
	{
		if (!Application.isPlaying)
		{
			return;
		}

		RenderSettings.fog = _enabled;
		RenderSettings.fogStartDistance = _startDistance;
		RenderSettings.fogEndDistance = _endDistance;
		RenderSettings.fogMode = _mode;
		RenderSettings.fogDensity = _density;
		RenderSettings.fogColor = _color;
	}
}
