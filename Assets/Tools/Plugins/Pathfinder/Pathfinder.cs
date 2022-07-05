using System;
using System.Collections.Generic;
using System.Linq;
using PriorityQueue;

namespace AStar
{
	public static class Pathfinder
	{
		private static void ReconstructPath(Dictionary<INode, INode> from, INode current, List<INode> path)
		{
			path.Clear();
			path.Add(current);

			while (from.Keys.Contains(current))
			{
				current = from[current];
				path.Add(current);
			}
		}

		public static bool GetPath(INode start, INode end, List<INode> path)
		{
			SimplePriorityQueue<INode> open = new SimplePriorityQueue<INode>();
			open.Enqueue(start, 0f);
			Dictionary<INode, INode> from = new Dictionary<INode, INode>();

			Dictionary<INode, float> gScore = new Dictionary<INode, float>
			{
				[start] = 0f
			};

			Dictionary<INode, float> fScore = new Dictionary<INode, float>
			{
				[start] = start.Estimate(end)
			};

			while (open.Count > 0)
			{
				INode current = open.Dequeue();
				if (current == end)
				{
					ReconstructPath(from, current, path);
					return true;
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

			return false;
		}
	}
}