using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get\u00b4s number of friends on Steam.")]
	[ActionCategory("steamworks.NET")]
	public class SteamGetFriendCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt friendCount;

		public override void Reset()
		{
			friendCount = null;
		}

		public override void OnEnter()
		{
			friendCount.Value = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
		}
	}
}
