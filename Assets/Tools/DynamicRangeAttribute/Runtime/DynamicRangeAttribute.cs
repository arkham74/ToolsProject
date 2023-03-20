using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using JD;
using Random = UnityEngine.Random;

namespace JD
{
	public class DynamicRangeAttribute : PropertyAttribute
	{
		public readonly string lowProperty;
		public readonly string highProperty;

		public DynamicRangeAttribute(string low, string high)
		{
			lowProperty = low;
			highProperty = high;
		}
	}
}