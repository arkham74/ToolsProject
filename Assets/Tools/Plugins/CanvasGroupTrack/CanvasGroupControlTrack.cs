using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(CanvasGroupControlAsset))]
[TrackBindingType(typeof(CanvasGroup))]
public class CanvasGroupControlTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<CanvasGroupControlMixerBehaviour>.Create(graph, inputCount);
	}
}
