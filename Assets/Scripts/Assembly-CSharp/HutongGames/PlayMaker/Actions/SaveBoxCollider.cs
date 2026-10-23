using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Easy Save 2")]
	[Tooltip("Saves a BoxCollider.")]
	public class SaveBoxCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject containing the BoxCollider we want to save.")]
		public FsmOwnerDefault saveValue;

		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		[RequiredField]
		public FsmString uniqueTag = string.Empty;

		[RequiredField]
		[Tooltip("The name of the file that we'll create to store our data.")]
		public FsmString saveFile = "defaultES2File.txt";

		public override void Reset()
		{
			uniqueTag = string.Empty;
			saveFile = "defaultES2File.txt";
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(saveValue);
			if (ownerDefaultTarget == null)
			{
				LogError("Could not save BoxCollider. No GameObject has been specified.");
				Finish();
				return;
			}
			BoxCollider component;
			if ((component = ownerDefaultTarget.GetComponent<BoxCollider>()) == null)
			{
				LogError("Could not save BoxCollider. GameObject does not contain a BoxCollider.");
				Finish();
				return;
			}
			ES2.Save(component, string.Concat(saveFile, "?tag=", uniqueTag));
			Log(string.Concat("Saved to ", saveFile, "?tag=", uniqueTag));
			Finish();
		}
	}
}
