using System;
using System.Collections.Generic;
using UnityEngine;

namespace JD
{
	public static class JsonHelper
	{
		public static T[] FromJson<T>(string json)
		{
			return JsonUtility.FromJson<Wrapper<T>>(json).items;
		}

		public static string ToJson<T>(params T[] array)
		{
			Wrapper<T> wrapper = new Wrapper<T> { items = array };
			return JsonUtility.ToJson(wrapper, false);
		}

		public static string ToJson<T>(bool prettyPrint, params T[] array)
		{
			Wrapper<T> wrapper = new Wrapper<T> { items = array };
			return JsonUtility.ToJson(wrapper, prettyPrint);
		}

		[Serializable]
		private class Wrapper<T>
		{
			public T[] items;
		}
	}
}