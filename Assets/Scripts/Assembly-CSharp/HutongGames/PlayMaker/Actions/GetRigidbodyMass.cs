using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("♥ Caligula ♥")]
	[Tooltip("Get material mass from the rigidbody")]
	public class GetRigidbodyMass : FsmStateAction
	{
		[Tooltip("Object")]
		public FsmGameObject go;

		[Tooltip("Mass of the rigidbody")]
		public FsmFloat mass;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			mass.Value = getMass(go.Value);
			Finish();
		}

		public override void OnUpdate()
		{
		}

		private float getMass(GameObject go)
		{
			float result = 0f;
			if (go.GetComponent<Rigidbody>() != null)
			{
				result = go.GetComponent<Rigidbody>().mass;
			}
			return result;
		}
	}
}
