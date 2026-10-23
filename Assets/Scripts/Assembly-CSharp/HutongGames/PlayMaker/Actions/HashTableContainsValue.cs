namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Check if a value exists in a PlayMaker HashTable Proxy component (PlayMakerHashTablePRoxy)")]
	public class HashTableContainsValue : HashTableActions
	{
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[RequiredField]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		[ActionSection("Set up")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component (necessary if several component coexists on the same GameObject)")]
		public FsmString reference;

		[Tooltip("The variable to check for.")]
		public FsmVar variable;

		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the test")]
		public FsmBool containsValue;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when value is found")]
		public FsmEvent valueFoundEvent;

		[Tooltip("The event to trigger when value is not found")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent valueNotFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			containsValue = null;
			valueFoundEvent = null;
			valueNotFoundEvent = null;
			variable = null;
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				doContainsValue();
			}
			Finish();
		}

		public void doContainsValue()
		{
			if (isProxyValid())
			{
				containsValue.Value = proxy.hashTable.ContainsValue(PlayMakerUtils.GetValueFromFsmVar(base.Fsm, variable));
				if (containsValue.Value)
				{
					base.Fsm.Event(valueFoundEvent);
				}
				else
				{
					base.Fsm.Event(valueNotFoundEvent);
				}
			}
		}
	}
}
