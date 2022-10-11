using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public class FlipBookAnimation : MonoBehaviour
	{
		public Image target;
		public float frameRate = 60;
		public bool loop = true;
		public List<Sprite> frames = new List<Sprite>();

		private float framefloat;

		private void Reset()
		{
			target = GetComponentInChildren<Image>();
		}

		private void Update()
		{
			framefloat += Time.deltaTime * frameRate;
			int frame = Mathf.FloorToInt(framefloat);

			if (loop)
			{
				target.sprite = frames.Repeat(frame);
			}
			else
			{
				target.sprite = frames.AtIndexClamp(frame);
			}
		}
	}
}