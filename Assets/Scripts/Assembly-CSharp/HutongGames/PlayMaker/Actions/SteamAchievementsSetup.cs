namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Sets up and Initializes the Steam Achievements.")]
	public class SteamAchievementsSetup : FsmStateAction
	{
		[RequiredField]
		[Tooltip("List of Achievement Ids.")]
		public FsmString[] AchievementIds;

		public override void Reset()
		{
			AchievementIds = null;
		}

		public override void OnEnter()
		{
			if (SteamManager.Initialized)
			{
				string[] array = new string[AchievementIds.Length];
				for (int i = 0; i < AchievementIds.Length; i++)
				{
					array[i] = AchievementIds[i].Value;
				}
				SteamManager.StatsAndAchievements.InitAchievements(array);
			}
			Finish();
		}
	}
}
