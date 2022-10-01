using UnityEngine;
using UnityEngine.Playables;

namespace JD.CanvasTrack
{
	public class CanvasGroupControlAsset : PlayableAsset
	{
		public CanvasGroupControlBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<CanvasGroupControlBehaviour>.Create(graph, template);
			return playable;
		}
	}
}