using System.IO;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Get Streaming Assets Path, Application Folder and persistentDataPath.")]
	public class GetStreamingAssetsPath : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Path Result")]
		[RequiredField]
		public FsmString DataPathResult;

		public FsmString AppFolderResult;

		public FsmString PersistentPathResult;

		[Tooltip("Platform Result Result")]
		[UIHint(UIHint.Variable)]
		public FsmString PlaformResult;

		public override void OnEnter()
		{
			string dataPath = Application.dataPath;
			string streamingAssetsPath = Application.streamingAssetsPath;
			string fullPath = Path.GetFullPath(".");
			dataPath += "/";
			streamingAssetsPath += "/";
			fullPath += "/";
			DataPathResult.Value = dataPath;
			AppFolderResult.Value = fullPath;
			PersistentPathResult.Value = streamingAssetsPath;
			PlaformResult.Value = Application.platform.ToString();
			Finish();
		}
	}
}
