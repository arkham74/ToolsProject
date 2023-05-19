using System.Collections;
using Freya;
using JD;
using UnityEngine;
using UnityEngine.Assertions;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace RTS
{
	public struct SpawnUnitAction : IAction
	{
		private Object prefab;
		private Vector3 position;

		public SpawnUnitAction(Object prefab, Vector3 position) : this()
		{
			this.prefab = prefab;
			this.position = position;
		}

		public IEnumerator Wait()
		{
			Assert.IsNotNull(prefab);
			Object.Instantiate(prefab, GridTools.WorldToGrid(position), Quaternion.identity);
			yield return Yield.WaitForEndOfFrame();
		}

		public override string ToString()
		{
			Assert.IsNotNull(prefab);
			return $"Spawn Unit: {prefab.name}";
		}
	}
}