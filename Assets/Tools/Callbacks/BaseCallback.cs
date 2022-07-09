using System;
using UnityEngine;

namespace CustomTools
{
	public abstract class BaseCallback : MonoBehaviour
	{
		[Flags]
		public enum State
		{
			AWAKE = 1,
			ON_ENABLE = 2,
			START = 4,
			ON_DISABLE = 8,
			ON_DESTROY = 16
		}

		public State state = State.START;
		protected abstract void Trigger();

		private void Awake()
		{
			if (state.HasFlag(State.AWAKE)) Trigger();
		}

		private void OnEnable()
		{
			if (state.HasFlag(State.ON_ENABLE)) Trigger();
		}

		private void Start()
		{
			if (state.HasFlag(State.START)) Trigger();
		}

		private void OnDisable()
		{
			if (state.HasFlag(State.ON_DISABLE)) Trigger();
		}

		private void OnDestroy()
		{
			if (state.HasFlag(State.ON_DESTROY)) Trigger();
		}
	}
}