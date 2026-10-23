namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Deletes data from MySQL Server via ES2.php file. See moodkie.com/easysave/WebSetup.php for how to set up MySQL.")]
	[ActionCategory("Easy Save 2")]
	public class DeleteFromWeb : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The URL to our ES2.PHP file. See http://www.moodkie.com/easysave/WebSetup.php for more information on setting up ES2Web")]
		public FsmString urlToPHPFile = "http://www.mysite.com/ES2.php";

		[RequiredField]
		[Tooltip("The username that you have specified in your ES2.php file.")]
		public FsmString username = "ES2";

		[RequiredField]
		[Tooltip("The password that you have specified in your ES2.php file.")]
		public FsmString password = "65w84e4p994z3Oq";

		[RequiredField]
		[Tooltip("The tag which we want to delete. Leave blank if deleting entire file.")]
		public FsmString tag = string.Empty;

		[RequiredField]
		[Tooltip("The name of the file that we want to either delete, or delete a tag from.")]
		public FsmString file = "defaultES2File.txt";

		[Tooltip("The Event to send if Delete succeeded.")]
		public FsmEvent isDeleted;

		[Tooltip("The event to send if Delete failed.")]
		public FsmEvent isError;

		[Tooltip("Where any errors thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorMessage = string.Empty;

		[Tooltip("Where any error codes thrown will be stored. Set this to a variable, or leave it blank.")]
		public FsmString errorCode = string.Empty;

		private ES2Web web;

		public override void Reset()
		{
			tag = string.Empty;
			file = "defaultES2File.txt";
			urlToPHPFile = "http://www.mysite.com/ES2.php";
			web = null;
			errorMessage = string.Empty;
			errorCode = string.Empty;
		}

		public override void OnEnter()
		{
			web = new ES2Web(string.Concat(urlToPHPFile, "?tag=", tag, "&webfilename=", file, "&webpassword=", password, "&webusername=", username));
			base.Fsm.Owner.StartCoroutine(web.Delete());
			Log(string.Concat("Web Deleting from ", urlToPHPFile, "?tag=", tag, "&webfilename=", file));
		}

		public override void OnUpdate()
		{
			if (web.isError)
			{
				errorMessage.Value = web.error;
				errorCode.Value = web.errorCode;
				base.Fsm.Event(isError);
			}
			else if (web.isDone)
			{
				base.Fsm.Event(isDeleted);
			}
		}
	}
}
