namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Forbids a mapped cInput key.")]
	[ActionCategory("cInput")]
	public class CInputForbidKeyString : FsmStateAction
	{
		[Tooltip("The name of the key to forbid.")]
		public FsmString keyName;

		public override void OnEnter()
		{
			cInput.ForbidKey(keyName.Value);
			Finish();
		}
	}
}
