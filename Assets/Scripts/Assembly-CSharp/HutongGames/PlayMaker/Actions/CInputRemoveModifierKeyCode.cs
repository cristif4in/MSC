using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("cInput")]
	[Tooltip("Removes a defined cInput modifier key.")]
	public class CInputRemoveModifierKeyCode : FsmStateAction
	{
		[Tooltip("The modifier key to add.")]
		public KeyCode keyName;

		public override void Reset()
		{
			keyName = KeyCode.None;
		}

		public override void OnEnter()
		{
			cInput.RemoveModifier(keyName);
			Finish();
		}
	}
}
