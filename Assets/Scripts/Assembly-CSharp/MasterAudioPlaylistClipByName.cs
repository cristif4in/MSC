using HutongGames.PlayMaker;

[Tooltip("Play a clip in the current Playlist by name in Master Audio")]
[ActionCategory(ActionCategory.Audio)]
public class MasterAudioPlaylistClipByName : FsmStateAction
{
	[Tooltip("Name of Playlist Controller to use. Not required if you only have one.")]
	public FsmString playlistControllerName;

	[Tooltip("Name of playlist clip to play.")]
	[RequiredField]
	public FsmString clipName;

	public override void OnEnter()
	{
		if (string.IsNullOrEmpty(playlistControllerName.Value))
		{
			MasterAudio.TriggerPlaylistClip(clipName.Value);
		}
		else
		{
			MasterAudio.TriggerPlaylistClip(playlistControllerName.Value, clipName.Value);
		}
		Finish();
	}

	public override void Reset()
	{
		clipName = new FsmString(string.Empty);
		playlistControllerName = new FsmString(string.Empty);
	}
}
