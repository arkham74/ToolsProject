using System;
using System.Collections.Generic;
using System.Linq;
using PriorityQueue;
using UnityEngine;

namespace Pathfinder
{
	public interface INode
	{
		public abstract float EnterCost { get; }
		public abstract float ExitCost { get; }
		public abstract Vector3 GetPosition();
		public abstract INode[] GetNeighbours();

		public virtual float Estimate(INode target)
		{
			return Vector3.Distance(GetPosition(), target.GetPosition());
		}

		public virtual float GetCost(INode node)
		{
			return node.EnterCost + ExitCost;
		}
	}

	public static class Pathfinder
	{
		private static List<INode> ReconstructPath(Dictionary<INode, INode> from, INode current)
		{
			List<INode> path = new List<INode>();
			path.Add(current);

			while (from.Keys.Contains(current))
			{
				current = from[current];
				path.Add(current);
			}

			return path;
		}

		public static bool TryGetPath(INode start, INode end, out List<INode> path)
		{
			path = GetPath(start, end);
			return path != null;
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