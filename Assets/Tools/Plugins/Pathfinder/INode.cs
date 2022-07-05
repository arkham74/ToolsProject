using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public interface INode
	{
		public abstract int EnterCost { get; }
		public abstract int ExitCost { get; }
		public abstract Vector3 GetPosition();
		public abstract List<INode> GetNeighbours();

		public virtual float Estimate(INode target)
		{
			return Vector3.Distance(GetPosition(), target.GetPosition());
		}

		public virtual float GetCost(INode node)
		{
			return node.EnterCost + ExitCost;
		}
	}
}