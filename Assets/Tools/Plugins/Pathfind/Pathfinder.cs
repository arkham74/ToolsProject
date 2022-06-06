using System;
using System.Collections.Generic;
using System.Linq;
using PriorityQueue;

namespace Pathfinder
{
	public interface INode
	{
		public abstract float Estimate(INode target);
		public abstract INode[] GetNeighbours();
		public abstract float GetCost(INode neighbor);
	}

	public static class Pathfinder
	{
		private static List<INode> ReconstructPath(Dictionary<INode, INode> from, INode current)
		{
			List<INode> path = new List<INode> { current };

			while (from.Keys.Contains(current))
			{
				current = from[current];
				path.Prepend(current);
			}

			return path;
		}

		public static List<INode> GetPath(INode start, INode end)
		{
			SimplePriorityQueue<INode> open = new SimplePriorityQueue<INode>();
			open.Enqueue(start, 0f);
			Dictionary<INode, INode> from = new Dictionary<INode, INode>();

			Dictionary<INode, float> gScore = new Dictionary<INode, float>();
			gScore[start] = 0f;

			Dictionary<INode, float> fScore = new Dictionary<INode, float>();
			fScore[start] = start.Estimate(end);

			while (open.Count > 0)
			{
				INode current = open.Dequeue();
				if (current == end)
				{
					return ReconstructPath(from, current);
				}
				open.Remove(current);

				foreach (INode neighbor in current.GetNeighbours())
				{
					float tentative_gScore = gScore.GetValueOrDefault(current, float.MaxValue) + current.GetCost(neighbor);
					if (tentative_gScore < gScore.GetValueOrDefault(neighbor, float.MaxValue))
					{
						from[neighbor] = current;
						gScore[neighbor] = tentative_gScore;
						fScore[neighbor] = tentative_gScore + neighbor.Estimate(end);

						if (!open.Contains(neighbor))
						{
							open.Enqueue(neighbor, fScore[neighbor]);
						}
					}
				}
			}

			return null;
		}
	}
}