using System.IO;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get Data Path, Application Folder and persistentDataPath.")]
	[ActionCategory(ActionCategory.Application)]
	public class GetAppPath : FsmStateAction
	{
		[Tooltip("Path Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString DataPathResult;

		public FsmString AppFolderResult;

		public FsmString PersistentPathResult;

		[Tooltip("Platform Result Result")]
		[UIHint(UIHint.Variable)]
		public FsmString PlaformResult;

		public override void OnEnter()
		{
			string dataPath = Application.dataPath;
			string persistentDataPath = Application.persistentDataPath;
			string fullPath = Path.GetFullPath(".");
			dataPath += "/";
			persistentDataPath += "/";
			fullPath += "/";
			DataPathResult.Value = dataPath;
			AppFolderResult.Value = fullPath;
			PersistentPathResult.Value = persistentDataPath;
			PlaformResult.Value = Application.platform.ToString();
			Finish();
		}
	}
}
