using HutongGames.PlayMaker;

[Tooltip("Stop all sound effects and Playlists in Master Audio.")]
[ActionCategory(ActionCategory.Audio)]
public class MasterAudioEverythingStop : FsmStateAction
{
	public override void OnEnter()
	{
		MasterAudio.StopEverything();
		Finish();
	}

	public override void Reset()
	{
	}
}
