namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Check if a key exists in a PlayMaker HashTable Proxy component (PlayMakerHashTablePRoxy)")]
	[ActionCategory("ArrayMaker/HashTable")]
	public class HashTableContains : HashTableActions
	{
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[Tooltip("The Key value to check for")]
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		public FsmString key;

		[Tooltip("Store the result of the test")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool containsKey;

		[Tooltip("The event to trigger when key is found")]
		[UIHint(UIHint.FsmEvent)]
		[ActionSection("Result")]
		public FsmEvent keyFoundEvent;

		[Tooltip("The event to trigger when key is not found")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent keyNotFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;
			containsKey = null;
			keyFoundEvent = null;
			keyNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				checkIfContainsKey();
			}
			Finish();
		}

		public void checkIfContainsKey()
		{
			if (isProxyValid())
			{
				containsKey.Value = proxy.hashTable.Contains(key.Value);
				if (containsKey.Value)
				{
					base.Fsm.Event(keyFoundEvent);
				}
				else
				{
					base.Fsm.Event(keyNotFoundEvent);
				}
			}
		}
	}
}
