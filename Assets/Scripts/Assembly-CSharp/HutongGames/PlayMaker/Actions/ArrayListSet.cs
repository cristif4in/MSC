namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Set an item at a specified index to a PlayMaker array List component")]
	public class ArrayListSet : ArrayListActions
	{
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[RequiredField]
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmString)]
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		public FsmString reference;

		[UIHint(UIHint.FsmString)]
		[Tooltip("The index of the Data in the ArrayList")]
		public FsmInt atIndex;

		public bool everyFrame;

		[ActionSection("Data")]
		[Tooltip("The variable to add.")]
		public FsmVar variable;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			variable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				SetToArrayList();
			}
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetToArrayList();
		}

		public void SetToArrayList()
		{
			if (isProxyValid())
			{
				proxy.Set(atIndex.Value, PlayMakerUtils.GetValueFromFsmVar(base.Fsm, variable), variable.Type.ToString());
			}
		}
	}
}
