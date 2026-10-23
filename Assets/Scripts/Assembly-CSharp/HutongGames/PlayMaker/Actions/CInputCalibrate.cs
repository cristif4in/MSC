namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Calibrates cInput.")]
	[ActionCategory("cInput")]
	public class CInputCalibrate : FsmStateAction
	{
		public override void OnEnter()
		{
			cInput.Calibrate();
			Finish();
		}
	}
}
