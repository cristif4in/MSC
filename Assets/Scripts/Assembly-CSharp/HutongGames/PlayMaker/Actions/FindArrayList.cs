using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Finds an ArrayList by reference. Warning: this function can be very slow.")]
	[ActionCategory("ArrayMaker/ArrayList")]
	public class FindArrayList : CollectionsActions
	{
		[UIHint(UIHint.FsmString)]
		[ActionSection("Set up")]
		[RequiredField]
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component")]
		public FsmString ArrayListReference;

		[Tooltip("Store the GameObject hosting the PlayMaker ArrayList Proxy component here")]
		[ActionSection("Result")]
		[RequiredField]
		public FsmGameObject store;

		public FsmEvent foundEvent;

		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			ArrayListReference = string.Empty;
			store = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			PlayMakerArrayListProxy[] array = Object.FindObjectsOfType(typeof(PlayMakerArrayListProxy)) as PlayMakerArrayListProxy[];
			PlayMakerArrayListProxy[] array2 = array;
			foreach (PlayMakerArrayListProxy playMakerArrayListProxy in array2)
			{
				if (playMakerArrayListProxy.referenceName == ArrayListReference.Value)
				{
					store.Value = playMakerArrayListProxy.gameObject;
					base.Fsm.Event(foundEvent);
					return;
				}
			}
			store.Value = null;
			base.Fsm.Event(notFoundEvent);
			Finish();
		}
	}
}
