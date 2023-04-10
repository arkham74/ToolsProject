using System.Collections;
using Redcode.Pools;
using UnityEngine;

namespace JD
{
	internal class SequencerBehaviour : MonoBehaviour
	{
		private static Pool<SequencerBehaviour> pool;

		private static void InitPool()
		{
			if (pool == null)
			{
				SequencerBehaviour prefab = new GameObject().AddComponent<SequencerBehaviour>();
				pool = Pool.Create<SequencerBehaviour>(prefab);
			}
		}

		internal static SequencerBehaviour Get()
		{
			InitPool();
			return pool.Get();
		}

		internal IEnumerator ReleaseAfter(Coroutine coroutine)
		{
			yield return coroutine;
			pool.Take(this);
		}
	}
}