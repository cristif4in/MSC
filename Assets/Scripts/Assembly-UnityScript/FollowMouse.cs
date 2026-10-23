using System;
using UnityEngine;

[Serializable]
public class FollowMouse : MonoBehaviour
{
	public float depth;

	public virtual void Update()
	{
		Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth - Camera.main.transform.position.z);
		Vector3 position2 = Camera.main.ScreenToWorldPoint(position);
		position2.z = depth;
		transform.position = position2;
	}

	public virtual void Main()
	{
	}
}
