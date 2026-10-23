namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Check if an ArrayList Proxy component exists.")]
	public class ArrayListExists : ArrayListActions
	{
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[ActionSection("Set up")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[Tooltip("Store in a bool wether it exists or not")]
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		public FsmBool doesExists;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Event sent if this arrayList exists ")]
		public FsmEvent doesExistsEvent;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Event sent if this arrayList does not exists")]
		public FsmEvent doesNotExistsEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			doesExists = null;
			doesExistsEvent = null;
			doesNotExistsEvent = null;
		}

		public override void OnEnter()
		{
			PlayMakerArrayListProxy arrayListProxyPointer = GetArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value, true);
			bool flag = arrayListProxyPointer != null;
			doesExists.Value = flag;
			if (flag)
			{
				base.Fsm.Event(doesExistsEvent);
			}
			else
			{
				base.Fsm.Event(doesNotExistsEvent);
			}
			Finish();
		}
	}
}
