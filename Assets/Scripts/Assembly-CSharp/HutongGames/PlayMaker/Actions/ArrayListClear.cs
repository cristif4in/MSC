namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Removes all elements from a PlayMaker ArrayList Proxy component")]
	public class ArrayListClear : ArrayListActions
	{
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[RequiredField]
		[ActionSection("Set up")]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				ClearArrayList();
			}
			Finish();
		}

		public void ClearArrayList()
		{
			if (isProxyValid())
			{
				proxy.arrayList.Clear();
			}
		}
	}
}
