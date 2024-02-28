using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[ExecuteAlways]
	public class FlipBookAnimation : MonoBehaviour
	{
		[SerializeField] private Image target;
		[SerializeField] private float frameRate = 60;
		[SerializeField] private bool loop = true;
		[SerializeField] private bool unscaledTime = false;
		[SerializeField] private bool reverse = false;
		[SerializeField] private List<Sprite> frames = new List<Sprite>();

		private float framefloat = 0;

		private void Reset()
		{
			target = GetComponentInChildren<Image>();
		}

		[Button]
		private void Restart()
		{
			framefloat = 0;
		}

		private void Update()
		{
			if (target && frames != null && frames.Count >= 1)
			{
				framefloat += GetTime() * frameRate;
				int floor = Mathf.FloorToInt(framefloat);
				int frame = GetFrame(floor);
				target.overrideSprite = GetSprite(frame);
			}
			else
			{
				target.overrideSprite = null;
			}
		}

		private Sprite GetSprite(int frame)
		{
			return loop ? frames.Repeat(frame) : frames.AtIndexClamp(frame);
		}

		private int GetFrame(int frame)
		{
			int count = frames.Count - 1;
			int reversed = count - frame;
			return reverse ? reversed : frame;
		}

		private float GetTime()
		{
			return unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
		}
	}
}