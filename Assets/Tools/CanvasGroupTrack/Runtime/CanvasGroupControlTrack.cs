#if TOOLS_TIMELINE
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JD.CanvasTrack
{
	[TrackClipType(typeof(CanvasGroupControlAsset))]
	[TrackBindingType(typeof(CanvasGroup))]
	public class CanvasGroupControlTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CanvasGroupControlMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
#endif