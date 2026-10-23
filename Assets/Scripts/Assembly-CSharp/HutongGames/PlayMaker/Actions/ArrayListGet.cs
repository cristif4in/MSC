using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets an item from a PlayMaker ArrayList Proxy component")]
	[ActionCategory("ArrayMaker/ArrayList")]
	public class ArrayListGet : ArrayListActions
	{
		[ActionSection("Set up")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[Tooltip("The index to retrieve the item from")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt atIndex;

		[UIHint(UIHint.Variable)]
		[ActionSection("Result")]
		public FsmVar result;

		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			atIndex = null;
			gameObject = null;
			failureEvent = null;
			result = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				GetItemAtIndex();
			}
			Finish();
		}

		public void GetItemAtIndex()
		{
			if (isProxyValid())
			{
				object obj = null;
				try
				{
					obj = proxy.arrayList[atIndex.Value];
				}
				catch (Exception ex)
				{
					Debug.Log(ex.Message);
					base.Fsm.Event(failureEvent);
					return;
				}
				PlayMakerUtils.ApplyValueToFsmVar(base.Fsm, result, obj);
			}
		}
	}
}
