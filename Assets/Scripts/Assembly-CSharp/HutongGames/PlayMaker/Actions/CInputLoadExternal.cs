namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("cInput")]
	[Tooltip("Loads external cInput settings from a string.")]
	public class CInputLoadExternal : FsmStateAction
	{
		[Tooltip("The string containing all the cInput settings.")]
		public FsmString cInputSettingsString;

		public override void OnEnter()
		{
			cInput.LoadExternal(cInputSettingsString.Value);
			Finish();
		}
	}
}
