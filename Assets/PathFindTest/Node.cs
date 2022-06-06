using System;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Pathfinder.Test
{
	public abstract class Node : MonoBehaviour, INode
	{
		private const float radius = 1.25f;

		[SerializeField] private Node[] neighs;
		[ShowNativeProperty] public abstract float EnterCost { get; }
		[ShowNativeProperty] public abstract float ExitCost { get; }

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, radius);
		}

		private void OnValidate()
		{
			float x = transform.position.x;
			float y = transform.position.y;

			name = $"Node [{x:0},{y:0}]";
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);
			neighs = cols.Select(e => e.GetComponent<Node>()).Except(new Node[] { this }).ToArray();
		}

		public INode[] GetNeighbours()
		{
			return neighs;
		}

		public Vector3 GetPosition()
		{
			return transform.position;
		}
	}
}