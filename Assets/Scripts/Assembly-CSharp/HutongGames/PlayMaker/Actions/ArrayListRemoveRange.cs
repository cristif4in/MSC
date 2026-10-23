using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Remove the specified range of elements from a PlayMaker ArrayList Proxy component")]
	public class ArrayListRemoveRange : ArrayListActions
	{
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[RequiredField]
		[ActionSection("Set up")]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[Tooltip("The zero-based index of the first element of the range of elements to remove. This value is between 0 and the array.count minus count (inclusive)")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt index;

		[Tooltip("The number of elements to remove. This value is between 0 and the difference between the array.count minus the index ( inclusive )")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt count;

		[Tooltip("The event to trigger if the removeRange throw errors")]
		[ActionSection("Result")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			index = null;
			count = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				doArrayListRemoveRange();
			}
			Finish();
		}

		public void doArrayListRemoveRange()
		{
			if (!isProxyValid())
			{
				return;
			}
			try
			{
				proxy.arrayList.RemoveRange(index.Value, count.Value);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				base.Fsm.Event(failureEvent);
			}
		}
	}
}
