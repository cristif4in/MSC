namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Print the value of an FSM Variable in the PlayMaker Log Window.")]
	[ActionCategory(ActionCategory.Debug)]
	public class DebugFsmVariable : FsmStateAction
	{
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		[HideTypeFilter]
		[Tooltip("Variable to print to the PlayMaker log window.")]
		[UIHint(UIHint.Variable)]
		public FsmVar fsmVar;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			fsmVar = null;
		}

		public override void OnEnter()
		{
			ActionHelpers.DebugLog(base.Fsm, logLevel, fsmVar.DebugString());
			Finish();
		}
	}
}
