using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Send\u00b4s message to friend by ID.")]
	[ActionCategory("steamworks.NET")]
	public class SteamSendMessage : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString friendID;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Message text as string.")]
		public FsmString message;

		[Tooltip("Return true if sended, return false if not possible.")]
		public FsmBool send;

		public override void Reset()
		{
			friendID = null;
			message = null;
		}

		public override void OnEnter()
		{
			ulong steamID = ulong.Parse(friendID.Value);
			CSteamID steamID2 = SteamUser.GetSteamID();
			steamID2.m_SteamID = steamID;
			send.Value = SteamFriends.ReplyToFriendMessage(steamID2, message.Value);
		}
	}
}
