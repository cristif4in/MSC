using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Web Player")]
	[Tooltip("Gets data from a url and store it in variables. See Unity WWW docs for more details.")]
	public class WWWObjectRelative : FsmStateAction
	{
		[Tooltip("Url to download data from.")]
		[RequiredField]
		public FsmString url;

		[Tooltip("If path is relative to Application datapath folder.")]
		public FsmBool pathIsRelative;

		[Tooltip("Gets text from the url.")]
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		public FsmString storeText;

		[Tooltip("Gets a Texture from the url.")]
		[UIHint(UIHint.Variable)]
		public FsmTexture storeTexture;

		[Tooltip("Gets Audio from the url.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AudioClip))]
		public FsmObject storeAudio;

		[ObjectType(typeof(MovieTexture))]
		[Tooltip("Gets a Texture from the url.")]
		[UIHint(UIHint.Variable)]
		public FsmObject storeMovieTexture;

		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[UIHint(UIHint.Variable)]
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		[Tooltip("Event to send when the data has finished loading (progress = 1).")]
		[ActionSection("Events")]
		public FsmEvent isDone;

		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		private WWW wwwObject;

		private string urlPrefix = "file://" + Application.dataPath + "/";

		public override void Reset()
		{
			url = null;
			pathIsRelative = false;
			storeText = null;
			storeTexture = null;
			errorString = null;
			progress = null;
			isDone = null;
		}

		public override void OnEnter()
		{
			if (string.IsNullOrEmpty(url.Value))
			{
				Finish();
				return;
			}
			bool flag = true;
			pathIsRelative.Value = flag;
			if (flag)
			{
				wwwObject = new WWW(urlPrefix + url.Value);
			}
			else
			{
				wwwObject = new WWW(url.Value);
			}
		}

		public override void OnUpdate()
		{
			if (wwwObject == null)
			{
				errorString.Value = "WWW Object is Null!";
				Finish();
				return;
			}
			errorString.Value = wwwObject.error;
			if (!string.IsNullOrEmpty(wwwObject.error))
			{
				Finish();
				base.Fsm.Event(isError);
				return;
			}
			progress.Value = wwwObject.progress;
			if (progress.Value.Equals(1f))
			{
				storeText.Value = wwwObject.text;
				storeTexture.Value = wwwObject.texture;
				storeAudio.Value = wwwObject.GetAudioClip();
				storeMovieTexture.Value = wwwObject.GetMovieTexture();
				errorString.Value = wwwObject.error;
				base.Fsm.Event((!string.IsNullOrEmpty(errorString.Value)) ? isError : isDone);
				Finish();
			}
		}
	}
}
