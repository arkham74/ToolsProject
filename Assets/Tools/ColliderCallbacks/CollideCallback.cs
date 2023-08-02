using UnityEngine;

#if ENABLE_INPUT_SYSTEM
#endif

namespace JD
{
	public class CollideCallback : ColliderCallback
	{
		private void OnCollisionEnter(Collision other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnEnter.Invoke(other.collider);
			}
		}

		private void OnCollisionExit(Collision other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnExit.Invoke(other.collider);
			}
		}

		private void OnCollisionStay(Collision other)
		{
			if (CheckTagAndLayer(other.collider))
			{
				OnStay.Invoke(other.collider);
			}
		}
	}
}