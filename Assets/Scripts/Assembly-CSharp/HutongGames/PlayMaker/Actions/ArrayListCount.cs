namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Count items from a PlayMaker ArrayList Proxy component")]
	public class ArrayListCount : ArrayListActions
	{
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[RequiredField]
		[ActionSection("Set up")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[Tooltip("Store the count value")]
		[ActionSection("Result")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt count;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			count = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				getArrayListCount();
			}
			Finish();
		}

		public void getArrayListCount()
		{
			if (isProxyValid())
			{
				int value = proxy.arrayList.Count;
				count.Value = value;
			}
		}
	}
}
