using SWS;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Simple Waypoint System")]
	[Tooltip("Gets the desired waypoint of a bezier path.")]
	public class SWS_GetWaypointOfPath : FsmStateAction
	{
		[Tooltip("Path manager component")]
		[ObjectType(typeof(PathManager))]
		[RequiredField]
		public FsmObject pathObject;

		[Tooltip("Waypoint index")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt wpIndex;

		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Waypoint gameobject")]
		public FsmGameObject waypoint;

		public override void Reset()
		{
			pathObject = null;
			wpIndex = null;
			waypoint = null;
		}

		public override void OnEnter()
		{
			Execute();
			Finish();
		}

		private void Execute()
		{
			PathManager pathManager = pathObject.Value as PathManager;
			if (wpIndex.Value > pathManager.waypoints.Length - 1)
			{
				wpIndex.Value = pathManager.waypoints.Length - 1;
			}
			if (pathManager is BezierPathManager)
			{
				waypoint.Value = (pathManager as BezierPathManager).bPoints[wpIndex.Value].wp.gameObject;
			}
			else
			{
				waypoint.Value = pathManager.waypoints[wpIndex.Value].gameObject;
			}
		}
	}
}
