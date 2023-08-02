using UnityEngine;

#if ENABLE_INPUT_SYSTEM
#endif

namespace JD
{
	public class TriggerCallback : ColliderCallback
	{
		private void OnTriggerEnter(Collider other)
		{
			if (CheckTagAndLayer(other))
			{
				OnEnter.Invoke(other);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (CheckTagAndLayer(other))
			{
				OnExit.Invoke(other);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (CheckTagAndLayer(other))
			{
				OnStay.Invoke(other);
			}
		}
	}
}