using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get\u00b4s steam user country.")]
	[ActionCategory("steamworks.NET")]
	public class SteamGetUserCountry : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmString userCountry;

		public override void Reset()
		{
			userCountry = null;
		}

		public override void OnEnter()
		{
			userCountry.Value = SteamUtils.GetIPCountry();
		}
	}
}
