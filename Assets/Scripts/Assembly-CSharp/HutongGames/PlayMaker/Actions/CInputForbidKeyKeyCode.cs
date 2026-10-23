using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Forbids a cInput key.")]
	[ActionCategory("cInput")]
	public class CInputForbidKeyKeyCode : FsmStateAction
	{
		[Tooltip("The name of the key to forbid.")]
		public KeyCode keyName;

		public override void OnEnter()
		{
			cInput.ForbidKey(keyName);
			Finish();
		}
	}
}
