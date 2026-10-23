namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Converts an String value to an Int value.")]
	[ActionCategory(ActionCategory.Convert)]
	public class ConvertStringToInt : FsmStateAction
	{
		[Tooltip("The String variable to convert to an integer.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		[Tooltip("Store the result in an Int variable.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		[Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			stringVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertStringToInt();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConvertStringToInt();
		}

		private void DoConvertStringToInt()
		{
			intVariable.Value = int.Parse(stringVariable.Value);
		}
	}
}
