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
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using System.Runtime.CompilerServices;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public static class IndexExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ForEachEnumerator GetEnumerator(this Range range)
		{
			return new ForEachEnumerator(range);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ForEachEnumerator GetEnumerator(this Index index)
		{
			return new ForEachEnumerator(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ForEachEnumerator GetEnumerator(this int index)
		{
			return new ForEachEnumerator(index);
		}

		public ref struct ForEachEnumerator
		{
			private readonly int _limit;
			private int _current;
			public int Current => _current - 1;

			public ForEachEnumerator(Range range)
			{
				if (range.End.IsFromEnd)
				{
					throw new NotSupportedException();
				}

				_current = range.Start.Value;
				_limit = range.End.Value;
			}

			public ForEachEnumerator(Index index)
			{
				if (index.IsFromEnd)
				{
					throw new NotSupportedException();
				}

				_current = 0;
				_limit = index.Value;
			}

			public ForEachEnumerator(int limit)
			{
				_current = 0;
				_limit = limit;
			}

			public bool MoveNext()
			{
				if (_current < _limit)
				{
					_current += 1;
					return true;
				}
				return false;
			}
		}
	}
}