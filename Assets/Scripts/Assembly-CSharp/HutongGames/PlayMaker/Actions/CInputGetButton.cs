namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets cInput button events down -> up -> pressed in order.")]
	[ActionCategory("cInput")]
	public class CInputGetButton : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the button.")]
		public FsmString buttonName;

		[Tooltip("The event that fires if the button gets down.")]
		public FsmEvent buttonDownEvent;

		[Tooltip("The event that fires if the button gets up.")]
		public FsmEvent buttonUpEvent;

		[Tooltip("The event that fires if the button is pressed (also on entering the action).")]
		public FsmEvent buttonPressedEvent;

		[Tooltip("update every frame or finish.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			buttonName = null;
			buttonDownEvent = null;
			buttonUpEvent = null;
			buttonPressedEvent = null;
			everyFrame = null;
		}

		public override void OnUpdate()
		{
			if (cInput.GetKeyDown(buttonName.Value))
			{
				base.Fsm.Event(buttonDownEvent);
			}
			else if (cInput.GetKeyUp(buttonName.Value))
			{
				base.Fsm.Event(buttonUpEvent);
			}
			else if (cInput.GetKey(buttonName.Value))
			{
				base.Fsm.Event(buttonPressedEvent);
			}
			if (!everyFrame.Value)
			{
				Finish();
			}
		}
	}
}
