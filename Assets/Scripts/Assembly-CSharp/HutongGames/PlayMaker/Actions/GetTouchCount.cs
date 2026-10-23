using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets the number of Touches.")]
	[ActionCategory(ActionCategory.Device)]
	public class GetTouchCount : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmInt storeCount;

		public bool everyFrame;

		public override void Reset()
		{
			storeCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetTouchCount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetTouchCount();
		}

		private void DoGetTouchCount()
		{
			storeCount.Value = Input.touchCount;
		}
	}
}
