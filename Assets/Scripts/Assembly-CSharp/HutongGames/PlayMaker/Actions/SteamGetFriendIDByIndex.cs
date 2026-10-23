using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get\u00b4s Friend ID by an int.")]
	[ActionCategory("steamworks.NET")]
	public class SteamGetFriendIDByIndex : FsmStateAction
	{
		[RequiredField]
		public FsmInt friendNumber;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString friendName;

		public override void Reset()
		{
			friendNumber = null;
			friendName = null;
		}

		public override void OnEnter()
		{
			CSteamID friendByIndex = SteamFriends.GetFriendByIndex(friendNumber.Value, EFriendFlags.k_EFriendFlagAll);
			friendName.Value = friendByIndex.ToString();
		}
	}
}
