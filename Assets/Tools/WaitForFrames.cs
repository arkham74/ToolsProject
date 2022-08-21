using UnityEngine;

namespace JD
{
	public sealed class WaitForFrames : CustomYieldInstruction
	{
		private int frame;
		private int startFrame;

		public WaitForFrames(int frames)
		{
			frame = frames;
			startFrame = Time.frameCount;
		}

		public override bool keepWaiting => (Time.frameCount - startFrame) < frame;
	}
}