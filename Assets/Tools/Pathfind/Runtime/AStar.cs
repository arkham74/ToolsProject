//https://www.redblobgames.com/pathfinding/a-star/introduction.html
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
using System.Runtime.InteropServices;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD.Pathfind
{
	public static class AStar<K> where K : IAStarNode<K>
	{
		private static readonly PriorityQueue<K> frontier = new PriorityQueue<K>();
		private static readonly AStarDictionary<K, K> came_from = new AStarDictionary<K, K>();
		private static readonly AStarDictionary<K, float> cost_so_far = new AStarDictionary<K, float>();

		public static List<K> GetPath(K start, K goal)
		{
			List<K> path = new List<K>();
			GetPathNonAlloc(start, goal, path);
			return path;
		}

		public static void GetPathNonAlloc(K start, K goal, List<K> path)
		{
			frontier.Clear();
			came_from.Clear();
			cost_so_far.Clear();

			frontier.Enqueue(start, 0);
			came_from.Add(start, default);
			cost_so_far.Add(start, 0);

			while (frontier.Count > 0)
			{
				K current = frontier.Dequeue();

				if (current.Equals(goal))
				{
					break;
				}

				IList<K> list = current.GetNeighbours();
				for (int i = 0; i < list.Count; i++)
				{
					K next = list[i];
					float new_cost = cost_so_far.Get(current) + next.GetCost();

					if (!came_from.ContainsKey(next))
					{
						cost_so_far.Add(next, new_cost);
						float priority = new_cost + goal.GetDistance(next);
						frontier.Enqueue(next, priority);
						came_from.Add(next, current);
					}
				}
			}

			K curr = goal;

			while (!curr.Equals(start))
			{
				path.Add(curr);
				curr = came_from.Get(curr);
			}

			path.Add(start);
			path.Reverse();
		}
	}
}