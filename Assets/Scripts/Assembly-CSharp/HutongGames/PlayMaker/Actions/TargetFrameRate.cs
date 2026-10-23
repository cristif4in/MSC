using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sets the target frame rate")]
	public class TargetFrameRate : FsmStateAction
	{
		[Tooltip("The target frame rate")]
		[RequiredField]
		public FsmInt targetFrameRate;

		public override void Reset()
		{
			targetFrameRate = 30;
		}

		public override void OnEnter()
		{
			Application.targetFrameRate = targetFrameRate.Value;
			Finish();
		}
	}
}
