using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Saves a PlayMaker HashTable Proxy component")]
	[ActionCategory("Easy Save 2")]
	public class HashTableEasySave : HashTableActions
	{
		[ActionSection("Set up")]
		[Tooltip("The Game Object to add the Hashtable Component to.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker arrayList proxy component ( necessary if several component coexists on the same GameObject")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[ActionSection("Easy Save Set Up")]
		[Tooltip("A unique tag for this save. For example, the object's name if no other objects use the same name.")]
		public FsmString uniqueTag = string.Empty;

		[Tooltip("The name of the file that we'll create to store our data.")]
		[RequiredField]
		public FsmString saveFile = "defaultES2File.txt";

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			uniqueTag = new FsmString
			{
				UseVariable = true
			};
			saveFile = "defaultES2File.txt";
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				SaveHashTable();
			}
			Finish();
		}

		public void SaveHashTable()
		{
			if (!isProxyValid())
			{
				return;
			}
			string text = uniqueTag.Value;
			if (string.IsNullOrEmpty(text))
			{
				text = base.Fsm.GameObjectName + "/" + base.Fsm.Name + "/hashTable/" + reference;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object key in proxy.hashTable.Keys)
			{
				dictionary[(string)key] = PlayMakerUtils.ParseValueToString(proxy.hashTable[key]);
			}
			ES2.Save(dictionary, saveFile.Value + "?tag=" + text);
			Log("Saved to " + saveFile.Value + "?tag=" + text);
			Finish();
		}
	}
}
