namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Gets items from a PlayMaker HashTable Proxy component")]
	public class HashTableGetMany : HashTableActions
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
		[CompoundArray("Count", "Key", "Value")]
		[ActionSection("Data")]
		public FsmString[] keys;

		[UIHint(UIHint.Variable)]
		[Tooltip("The value for that key")]
		public FsmVar[] results;

		public override void Reset()
		{
			gameObject = null;
			keys = null;
			results = null;
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
			if (!isProxyValid())
			{
				return;
			}
			for (int i = 0; i < keys.Length; i++)
			{
				if (proxy.hashTable.ContainsKey(keys[i].Value))
				{
					PlayMakerUtils.ApplyValueToFsmVar(base.Fsm, results[i], proxy.hashTable[keys[i].Value]);
				}
			}
		}
	}
}
