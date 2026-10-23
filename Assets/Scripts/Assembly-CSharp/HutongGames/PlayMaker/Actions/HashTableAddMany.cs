namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Add key/value pairs to a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy).")]
	[ActionCategory("ArrayMaker/HashTable")]
	public class HashTableAddMany : HashTableActions
	{
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[RequiredField]
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[ActionSection("Data")]
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("The Key")]
		public FsmString[] keys;

		[Tooltip("The value for that key")]
		[RequiredField]
		public FsmVar[] variables;

		[Tooltip("The event to trigger when elements are added")]
		[UIHint(UIHint.FsmEvent)]
		[ActionSection("Result")]
		public FsmEvent successEvent;

		[Tooltip("The event to trigger when elements exists already")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent keyExistsAlreadyEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			keys = null;
			variables = null;
			successEvent = null;
			keyExistsAlreadyEvent = null;
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				if (keyExistsAlreadyEvent != null)
				{
					FsmString[] array = keys;
					foreach (FsmString fsmString in array)
					{
						if (proxy.hashTable.ContainsKey(fsmString.Value))
						{
							base.Fsm.Event(keyExistsAlreadyEvent);
							Finish();
						}
					}
				}
				AddToHashTable();
				base.Fsm.Event(successEvent);
			}
			Finish();
		}

		public void AddToHashTable()
		{
			if (isProxyValid())
			{
				for (int i = 0; i < keys.Length; i++)
				{
					proxy.hashTable.Add(keys[i].Value, PlayMakerUtils.GetValueFromFsmVar(base.Fsm, variables[i]));
				}
			}
		}
	}
}
