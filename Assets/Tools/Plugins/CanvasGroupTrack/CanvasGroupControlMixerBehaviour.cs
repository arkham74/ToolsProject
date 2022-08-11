using UnityEngine;
using UnityEngine.Playables;

namespace JD.CanvasTrack
{
	public class CanvasGroupControlMixerBehaviour : PlayableBehaviour
	{
		// NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			CanvasGroup trackBinding = playerData as CanvasGroup;
			float finalAlpha = 0f;

			if (trackBinding)
			{
				int inputCount = playable.GetInputCount(); //get the number of all clips on this track

				for (int i = 0; i < inputCount; i++)
				{
					float inputWeight = playable.GetInputWeight(i);
					ScriptPlayable<CanvasGroupControlBehaviour> inputPlayable = (ScriptPlayable<CanvasGroupControlBehaviour>)playable.GetInput(i);
					CanvasGroupControlBehaviour input = inputPlayable.GetBehaviour();

					// Use the above variables to process each frame of this playable.
					finalAlpha += input.alpha * inputWeight;
				}

				//assign the result to the bound object
				trackBinding.alpha = finalAlpha;
			}
		}
	}
}