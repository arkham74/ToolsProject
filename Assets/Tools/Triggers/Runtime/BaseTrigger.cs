using System;
using UnityEngine;

namespace JD
{
	public abstract class BaseTrigger : MonoBehaviour
	{
		[Flags]
		public enum State
		{
			Awake = 1,
			Enable = 2,
			Start = 4,
			Disable = 8,
			Destroy = 16
		}

		public State state = State.Start;
		protected abstract void Trigger();

		private void Awake()
		{
			if (state.HasFlag(State.Awake))
			{
				Trigger();
			}
		}

		private void OnEnable()
		{
			if (state.HasFlag(State.Enable))
			{
				Trigger();
			}
		}

		private void Start()
		{
			if (state.HasFlag(State.Start))
			{
				Trigger();
			}
		}

		private void OnDisable()
		{
			if (state.HasFlag(State.Disable))
			{
				Trigger();
			}
		}

		private void OnDestroy()
		{
			if (state.HasFlag(State.Destroy))
			{
				Trigger();
			}
		}
	}
}
