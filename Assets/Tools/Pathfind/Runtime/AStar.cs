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
	public interface IAStarNode<T> : IEquatable<T>, IEqualityComparer<T>
	{
		public IList<T> GetNeighbours();
		public float GetCost();
		public float GetDistance(T target);
	}

	public static class AStar<T> where T : IAStarNode<T>
	{
		private static readonly PriorityQueue<T> frontier = new PriorityQueue<T>();
		private static readonly Dictionary<T, T> came_from = new Dictionary<T, T>();
		private static readonly Dictionary<T, float> cost_so_far = new Dictionary<T, float>();

		public static List<T> GetPath(T start, T goal)
		{
			List<T> path = new List<T>();
			GetPathNonAlloc(start, goal, path);
			return path;
		}

		public static void GetPathNonAlloc(T start, T goal, List<T> path)
		{
			frontier.Clear();
			came_from.Clear();
			cost_so_far.Clear();

			frontier.Enqueue(start, 0);
			came_from[start] = default;
			cost_so_far[start] = 0;

			while (frontier.Count > 0)
			{
				T current = frontier.Dequeue();

				if (current.Equals(goal))
				{
					break;
				}

				IList<T> list = current.GetNeighbours();
				for (int i = 0; i < list.Count; i++)
				{
					T next = list[i];
					float new_cost = cost_so_far[current] + next.GetCost();

					if (!came_from.ContainsKey(next))
					{
						cost_so_far[next] = new_cost;
						float priority = new_cost + goal.GetDistance(next);
						frontier.Enqueue(next, priority);
						came_from[next] = current;
					}
				}
			}

			T curr = goal;

			while (!curr.Equals(start))
			{
				path.Add(curr);
				curr = came_from[curr];
			}

			path.Add(start);
			path.Reverse();
		}

		// private static K GetItem<K>(Dictionary<T, K> dict, T key)
		// {
		// 	return dict[key];
		// }

		// private static bool ContainsKey(Dictionary<T, T> dict, T key)
		// {
		// 	foreach ((T v, T k) in dict)
		// 	{
		// 		if (k.Equals(key))
		// 		{
		// 			return true;
		// 		}
		// 	}
		// 	return false;
		// }
	}
}