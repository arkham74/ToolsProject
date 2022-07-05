using System;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using System.Collections.Generic;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AStar.Test
{
	public abstract class Node : MonoBehaviour, INode
	{
		private const float radius = 1.25f;
		[SerializeField] private List<Node> neighs;
		[ShowNativeProperty] public abstract int EnterCost { get; }
		[ShowNativeProperty] public abstract int ExitCost { get; }

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
			neighs = cols.Select(e => e.GetComponent<Node>()).Except(new Node[] { this }).ToList();
		}

		public List<INode> GetNeighbours()
		{
			return neighs.ToList<INode>();
		}

		public Vector3 GetPosition()
		{
			return transform.position;
		}
	}
}