using UnityEngine;

namespace JD
{
	public sealed class WaitForFrames : CustomYieldInstruction
	{
		private readonly int frame;
		private readonly int startFrame;

		public WaitForFrames(int frames)
		{
			frame = frames;
			startFrame = Time.frameCount;
		}

		public override bool keepWaiting => Time.frameCount - startFrame < frame;
	}
}
