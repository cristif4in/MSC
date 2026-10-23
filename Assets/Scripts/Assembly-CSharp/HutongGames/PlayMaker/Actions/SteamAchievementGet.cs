namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Gets the display name, description, and status of an achievement.")]
	public class SteamAchievementGet : FsmStateAction
	{
		[Tooltip("This is the 'API Name' in the Steam Partner backend")]
		[RequiredField]
		public FsmString achievementId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the Achievement Name.")]
		public FsmString achievementName;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the Achievement Description.")]
		public FsmString achievementDescription;

		[Tooltip("True if the Achievement is unlocked, False if it is locked.")]
		[UIHint(UIHint.Variable)]
		public FsmBool achievementUnlocked;

		public override void Reset()
		{
			achievementId = null;
			achievementName = null;
			achievementDescription = null;
			achievementUnlocked = null;
		}

		public override void OnEnter()
		{
			Achievement_t achievement = SteamManager.StatsAndAchievements.GetAchievement(achievementId.Value);
			if (achievement != null)
			{
				achievementName.Value = achievement.m_strName;
				achievementDescription.Value = achievement.m_strDescription;
				achievementUnlocked.Value = achievement.m_bAchieved;
			}
			Finish();
		}
	}
}
