using HutongGames.PlayMaker;

[Tooltip("Pause all sound effects in Master Audio. Does not include Playlists.")]
[ActionCategory(ActionCategory.Audio)]
public class MasterAudioMixerPause : FsmStateAction
{
	public override void OnEnter()
	{
		MasterAudio.PauseMixer();
		Finish();
	}

	public override void Reset()
	{
	}
}
