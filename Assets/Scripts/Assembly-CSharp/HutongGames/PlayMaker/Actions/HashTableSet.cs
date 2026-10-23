namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Set an key/value pair to a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy)")]
	[ActionCategory("ArrayMaker/HashTable")]
	public class HashTableSet : HashTableActions
	{
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[ActionSection("Set up")]
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
		[Tooltip("The variable to set.")]
		public FsmVar variable;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;
			variable = null;
		}

		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				SetHashTable();
			}
			Finish();
		}

		public void SetHashTable()
		{
			if (isProxyValid())
			{
				proxy.hashTable[key.Value] = PlayMakerUtils.GetValueFromFsmVar(base.Fsm, variable);
			}
		}
	}
}
