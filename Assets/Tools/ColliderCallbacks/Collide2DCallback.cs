using UnityEngine;

#if ENABLE_INPUT_SYSTEM
#endif

namespace JD
{
	public class Collide2DCallback : Collider2DCallback
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnEnter.Invoke(other.collider);
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnExit.Invoke(other.collider);
			}
		}

		private void OnCollisionStay2D(Collision2D other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnStay.Invoke(other.collider);
			}
		}
	}
}