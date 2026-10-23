using HutongGames.PlayMaker;

[Tooltip("Mute all sound effects and Playlists in Master Audio.")]
[ActionCategory(ActionCategory.Audio)]
public class MasterAudioEverythingMute : FsmStateAction
{
	public override void OnEnter()
	{
		MasterAudio.MuteEverything();
		Finish();
	}

	public override void Reset()
	{
	}
}
