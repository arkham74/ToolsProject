using System;
using System.Collections.Generic;
using System.Linq;
using PriorityQueue;
using UnityEngine;

public static class Pathfinder
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

		Dictionary<INode, float> gScore = new Dictionary<INode, float>();
		gScore[start] = 0f;

		Dictionary<INode, float> fScore = new Dictionary<INode, float>();
		fScore[start] = start.Estimate(end);

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