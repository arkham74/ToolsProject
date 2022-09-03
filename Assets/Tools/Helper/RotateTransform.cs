using UnityEngine;

namespace JD
{
	public class RotateTransform : MonoBehaviour
	{
		public Transform target;
		public float speed = 10;
		public Vector3 axis = Vector3.up;
		public bool localRotation;

		private void Reset()
		{
			target = transform;
		}

		private void Update()
		{
			float angle = Time.realtimeSinceStartup * speed;
			if (localRotation)
			{
				target.localRotation = Quaternion.Euler(axis * angle);
			}
			else
			{
				target.rotation = Quaternion.Euler(axis * angle);
			}
		}
	}
}
