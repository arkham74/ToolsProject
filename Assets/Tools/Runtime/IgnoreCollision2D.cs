using UnityEngine;

namespace JD
{
	public class IgnoreCollision2D : MonoBehaviour
	{
		[SerializeField] private bool ignore = true;
		[SerializeField] private Collider2D collider1;
		[SerializeField] private Collider2D collider2;

		private void Awake()
		{
			Physics2D.IgnoreCollision(collider1, collider2, ignore);
		}
	}
}