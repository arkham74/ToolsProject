using System;
using System.Collections.Generic;

namespace JD.Pathfind
{
	public interface IAStarNode<T> : IEquatable<T>
	{
		public IList<T> GetNeighbours();
		public float GetCost();
		public float GetDistance(T target);
	}
}