using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Freya;

#if TOOLS_NAUATTR
using NaughtyAttributes;
using Tag = NaughtyAttributes.TagAttribute;
#endif

namespace JD
{
	public class SpriteAnimator : MonoBehaviour
	{
		public SpriteRenderer target;
		public float frameRate = 12;
		public List<Sprite> frames = new List<Sprite>();
		private float time;

		protected void Reset()
		{
			target = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			if (frames.Count > 0)
			{
				time += Time.deltaTime * frameRate;
				int index = Mathf.RoundToInt(time);
				target.sprite = frames.Repeat(index);
			}
		}
	}
}