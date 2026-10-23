namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Add several items to a PlayMaker Array List Proxy component")]
	[ActionCategory("ArrayMaker/ArrayList")]
	public class ArrayListAddRange : ArrayListActions
	{
		[ActionSection("Set up")]
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[Tooltip("The variables to add.")]
		[ActionSection("Data")]
		[RequiredField]
		public FsmVar[] variables;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			variables = new FsmVar[2];
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				DoArrayListAddRange();
			}
			Finish();
		}

		public void DoArrayListAddRange()
		{
			if (isProxyValid())
			{
				FsmVar[] array = variables;
				foreach (FsmVar fsmVar in array)
				{
					proxy.Add(PlayMakerUtils.GetValueFromFsmVar(base.Fsm, fsmVar), fsmVar.Type.ToString(), true);
				}
			}
		}
	}
}
