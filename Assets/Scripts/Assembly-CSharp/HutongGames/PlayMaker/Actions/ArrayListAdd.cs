namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Add an item to a PlayMaker Array List Proxy component")]
	[ActionCategory("ArrayMaker/ArrayList")]
	public class ArrayListAdd : ArrayListActions
	{
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmString)]
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		public FsmString reference;

		[RequiredField]
		[ActionSection("Data")]
		[Tooltip("The variable to add.")]
		public FsmVar variable;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			variable = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				AddToArrayList();
			}
			Finish();
		}

		public void AddToArrayList()
		{
			if (isProxyValid())
			{
				proxy.Add(PlayMakerUtils.GetValueFromFsmVar(base.Fsm, variable), variable.Type.ToString());
			}
		}
	}
}
