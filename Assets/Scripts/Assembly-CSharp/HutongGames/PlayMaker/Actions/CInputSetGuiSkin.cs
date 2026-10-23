using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Sets the cGUI skin")]
	[ActionCategory("cInput")]
	public class CInputSetGuiSkin : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The gui to set for cGUI.")]
		public GUISkin guiSkin;

		public override void OnEnter()
		{
			cGUI.cSkin = guiSkin;
			Finish();
		}
	}
}
