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
using TMPro;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public abstract class RuntimeSet<T> : ScriptableObject, IList<T> where T : UnityEngine.Object
	{
		[SerializeField] protected List<T> list = new List<T>();

#if UNITY_EDITOR
		private void Reset()
		{
			list.Clear();
			list.AddRange(AssetTools.FindAssetsByType<T>());
		}
#endif

		public T this[int index] { get => list[index]; set => list[index] = value; }
		public int Count => list.Count;
		public int Lenght => list.Count;
		public bool IsReadOnly => false;
		public void Add(T item) => list.Add(item);
		public void Clear() => list.Clear();
		public bool Contains(T item) => list.Contains(item);
		public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
		public int IndexOf(T item) => list.IndexOf(item);
		public void Insert(int index, T item) => list.Insert(index, item);
		public bool Remove(T item) => list.Remove(item);
		public void RemoveAt(int index) => list.RemoveAt(index);
		public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
	}
}