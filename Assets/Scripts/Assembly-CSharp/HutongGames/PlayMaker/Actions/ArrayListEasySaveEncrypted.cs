using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Saves a PlayMaker Array List Proxy component")]
	[ActionCategory("Easy Save 2")]
	public class ArrayListEasySaveEncrypted : ArrayListActions
	{
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[RequiredField]
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		[ActionSection("Easy Save Set Up")]
		public FsmString uniqueTag = string.Empty;

		[Tooltip("Activate Decryption")]
		public FsmBool encryption;

		[Tooltip("the password of the saved item")]
		public FsmString password = string.Empty;

		[Tooltip("The name of the file that we'll create to store our data.")]
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			encryption = false;
			password = string.Empty;
			uniqueTag = new FsmString
			{
				UseVariable = true
			};
			saveFile = "defaultES2File.txt";
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				SaveArrayList();
			}
			Finish();
		}

		public void SaveArrayList()
		{
			if (!isProxyValid())
			{
				return;
			}
			string text = "false";
			if (encryption.Value)
			{
				text = "true";
			}
			List<string> list = new List<string>();
			foreach (object array in proxy.arrayList)
			{
				list.Add(PlayMakerUtils.ParseValueToString(array));
			}
			ES2.Save(list, string.Concat(saveFile.Value, "?tag=", uniqueTag, "&encrypt=", text, "&password=", password.Value));
			Log("Saved to " + saveFile.Value + "?tag=" + uniqueTag);
			Debug.LogWarning("Persistent Data Path:" + Application.persistentDataPath);
			Finish();
		}
	}
}
