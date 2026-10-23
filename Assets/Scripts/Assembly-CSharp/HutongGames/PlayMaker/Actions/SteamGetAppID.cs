using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get\u00b4s steam app ID.")]
	[ActionCategory("steamworks.NET")]
	public class SteamGetAppID : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmString appID;

		public override void Reset()
		{
			appID = null;
		}

		public override void OnEnter()
		{
			AppId_t appId_t = SteamUtils.GetAppID();
			appID.Value = appId_t.ToString();
		}
	}
}
