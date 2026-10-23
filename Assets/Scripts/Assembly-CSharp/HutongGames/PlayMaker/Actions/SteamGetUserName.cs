using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get\u00b4s steam user name.")]
	[ActionCategory("steamworks.NET")]
	public class SteamGetUserName : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString userName;

		public override void Reset()
		{
			userName = null;
		}

		public override void OnEnter()
		{
			userName.Value = SteamFriends.GetPersonaName();
		}
	}
}
