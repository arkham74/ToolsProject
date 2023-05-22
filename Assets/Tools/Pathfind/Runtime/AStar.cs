//https://www.redblobgames.com/pathfinding/a-star/introduction.html
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;
using System.IO;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD.Pathfind
{
	public interface IAStarNode
	{
		public IAStarNode[] GetNeighbours();
		public float GetCost(IAStarNode target);
		public float GetDistance(IAStarNode target);
	}

	public static class AStar
	{
		private static readonly PriorityQueue<IAStarNode, float> frontier = new PriorityQueue<IAStarNode, float>();
		private static readonly Dictionary<IAStarNode, IAStarNode> came_from = new Dictionary<IAStarNode, IAStarNode>();
		private static readonly Dictionary<IAStarNode, float> cost_so_far = new Dictionary<IAStarNode, float>();

		public static void GetPath(IAStarNode start, IAStarNode goal, List<IAStarNode> path)
		{
			frontier.Clear();
			came_from.Clear();
			cost_so_far.Clear();

			frontier.Enqueue(start, 0);
			came_from[start] = null;
			cost_so_far[start] = 0;

			while (frontier.Count > 0)
			{
				IAStarNode current = frontier.Dequeue();

				if (current == goal)
				{
					break;
				}

				foreach (IAStarNode next in current.GetNeighbours())
				{
					float new_cost = cost_so_far[current] + current.GetCost(next);

					if (!came_from.ContainsKey(next))
					{
						cost_so_far[next] = new_cost;
						float priority = new_cost + goal.GetDistance(next);
						frontier.Enqueue(next, priority);
						came_from[next] = current;
					}
				}
			}

			IAStarNode curr = goal;

			while (curr != start)
			{
				path.Add(curr);
				curr = came_from[curr];
			}

			path.Add(start);
			path.Reverse();
		}
	}
}