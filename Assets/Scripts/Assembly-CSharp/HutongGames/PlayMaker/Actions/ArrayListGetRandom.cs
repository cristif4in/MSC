using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets a random item from a PlayMaker ArrayList Proxy component")]
	[ActionCategory("ArrayMaker/ArrayList")]
	public class ArrayListGetRandom : ArrayListActions
	{
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[RequiredField]
		[ActionSection("Set up")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[UIHint(UIHint.Variable)]
		[Tooltip("The random item data picked from the array")]
		[ActionSection("Result")]
		public FsmVar randomItem;

		[UIHint(UIHint.Variable)]
		[Tooltip("The random item index picked from the array")]
		public FsmInt randomIndex;

		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			gameObject = null;
			failureEvent = null;
			randomItem = null;
			randomIndex = null;
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				GetRandomItem();
			}
			Finish();
		}

		public void GetRandomItem()
		{
			if (isProxyValid())
			{
				int num = UnityEngine.Random.Range(0, proxy.arrayList.Count);
				object obj = null;
				try
				{
					obj = proxy.arrayList[num];
				}
				catch (Exception ex)
				{
					Debug.LogWarning(ex.Message);
					base.Fsm.Event(failureEvent);
					return;
				}
				randomIndex.Value = num;
				if (!PlayMakerUtils.ApplyValueToFsmVar(base.Fsm, randomItem, obj))
				{
					Debug.LogWarning("ApplyValueToFsmVar failed");
					base.Fsm.Event(failureEvent);
				}
			}
		}
	}
}
