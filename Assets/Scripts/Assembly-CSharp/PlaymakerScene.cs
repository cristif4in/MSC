using UnityEngine;

public class PlaymakerScene : MonoBehaviour
{
	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 40f, 450f, 160f), "Click on the player to play a sound. Move him around before clicking to hear the sound generated from different locations.");
	}
}
