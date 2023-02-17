using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace JD
{
	public struct DebugTimer : IDisposable
	{
		private long ticks;

		public static DebugTimer Record()
		{
			return new DebugTimer(Stopwatch.GetTimestamp());
		}

		public DebugTimer(long ticks)
		{
			this.ticks = ticks;
		}

		public void Dispose()
		{
			long value = Stopwatch.GetTimestamp() - ticks;
			TimeSpan timeSpan = TimeSpan.FromTicks(value);
			double totalMilliseconds = timeSpan.TotalMilliseconds;
			Debug.LogWarning(totalMilliseconds);
		}
	}
}