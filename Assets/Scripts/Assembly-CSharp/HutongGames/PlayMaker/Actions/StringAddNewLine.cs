namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Adds strings together separated by a line.")]
	public class StringAddNewLine : FsmStateAction
	{
		[RequiredField]
		[Tooltip("List of the strings to compose.")]
		public FsmString[] stringParts;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Store the result.")]
		public FsmString storeResult;

		public override void Reset()
		{
			stringParts = new FsmString[2];
			storeResult = null;
		}

		public override void OnEnter()
		{
			DoBuildString();
			Finish();
		}

		private void DoBuildString()
		{
			if (storeResult == null)
			{
				return;
			}
			string text = string.Empty;
			int num = stringParts.Length;
			for (int i = 0; i < num; i++)
			{
				if (text != string.Empty)
				{
					text += "\n";
				}
				text += stringParts[i];
			}
			storeResult.Value = text;
		}
	}
}
