using UnityEngine;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
#endif

namespace JD
{
	public abstract class Collider2DCallback : BaseCallback
	{
		[SerializeField] protected UnityEvent<Collider2D> OnEnter;
		[SerializeField] protected UnityEvent<Collider2D> OnExit;
		[SerializeField] protected UnityEvent<Collider2D> OnStay;

		protected bool CheckTagAndLayer(Collider2D other)
		{
			if (objectTag == string.Empty || other.CompareTag(objectTag))
			{
				int mask = 1 << other.gameObject.layer;
				if ((objectMask & mask) != 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}