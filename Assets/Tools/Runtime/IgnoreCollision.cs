using UnityEngine;

namespace JD
{
	public class IgnoreCollision : MonoBehaviour
	{
		[SerializeField] private bool ignore = true;
		[SerializeField] private Collider collider1;
		[SerializeField] private Collider collider2;

		private void Awake()
		{
			Physics.IgnoreCollision(collider1, collider2, ignore);
		}
	}
}