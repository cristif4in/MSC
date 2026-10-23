namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Uploads a score to a Steam Leaderboard.")]
	[ActionCategory("steamworks.NET")]
	public class SteamUploadScore : FsmStateAction
	{
		[Tooltip("Leaderboard name.")]
		[RequiredField]
		public FsmString leaderboardName;

		[Tooltip("The score to upload.")]
		[RequiredField]
		public FsmInt score;

		public override void Reset()
		{
			leaderboardName = null;
			score = null;
		}

		public override void OnEnter()
		{
			if (SteamManager.Initialized)
			{
				SteamManager.Leaderboards.UploadScore(leaderboardName.Value, score.Value);
			}
			Finish();
		}
	}
}
