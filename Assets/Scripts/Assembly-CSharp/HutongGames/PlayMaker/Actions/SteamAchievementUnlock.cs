namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Unlocks an Achievement by name.")]
	public class SteamAchievementUnlock : FsmStateAction
	{
		[Tooltip("Achievement name.")]
		[RequiredField]
		public FsmString achievementId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns true on success, false on failure.")]
		public FsmBool success;

		public override void Reset()
		{
			achievementId = null;
			success = null;
		}

		public override void OnEnter()
		{
			success.Value = SteamManager.StatsAndAchievements.UnlockAchievement(achievementId.Value);
		}
	}
}
