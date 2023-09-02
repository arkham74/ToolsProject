using System;
using JD;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	[ExecuteAlways]
	public abstract class ColorSync : MonoBehaviour
	{
		public abstract void Apply();
	}

	[ExecuteAlways]
	public abstract class ColorSync<TData> : ColorSync where TData : ColorData
	{
		[SerializeField][Expandable] protected TData colorData;

		private void OnEnable()
		{
			if (colorData)
			{
				colorData.OnChange += Apply;
			}
		}

		private void OnDisable()
		{
			if (colorData)
			{
				colorData.OnChange -= Apply;
			}
		}
	}
}