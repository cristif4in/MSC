namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets an item from a PlayMaker HashTable Proxy component")]
	[ActionCategory("ArrayMaker/HashTable")]
	public class HashTableGet : HashTableActions
	{
		[ActionSection("Set up")]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[UIHint(UIHint.FsmString)]
		[Tooltip("The Key value for that hash set")]
		[RequiredField]
		public FsmString key;

		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		public FsmVar result;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when key is found")]
		public FsmEvent KeyFoundEvent;

		[Tooltip("The event to trigger when key is not found")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent KeyNotFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			key = null;
			KeyFoundEvent = null;
			KeyNotFoundEvent = null;
			result = null;
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				Get();
			}
			Finish();
		}

		public void Get()
		{
			if (isProxyValid())
			{
				if (!proxy.hashTable.ContainsKey(key.Value))
				{
					base.Fsm.Event(KeyNotFoundEvent);
					return;
				}
				PlayMakerUtils.ApplyValueToFsmVar(base.Fsm, result, proxy.hashTable[key.Value]);
				base.Fsm.Event(KeyFoundEvent);
			}
		}
	}
}
