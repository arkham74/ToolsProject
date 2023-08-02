using UnityEngine;

#if ENABLE_INPUT_SYSTEM
#endif

namespace JD
{
	public class Trigger2DCallback : Collider2DCallback
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (CheckTagAndLayer(other))
			{
				OnEnter.Invoke(other);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (CheckTagAndLayer(other))
			{
				OnExit.Invoke(other);
			}
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if (CheckTagAndLayer(other))
			{
				OnStay.Invoke(other);
			}
		}
	}
}