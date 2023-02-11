using System;
using System.Linq;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections.Generic;

namespace JD
{
	public static class OtherExtensions
	{
		public static void BreadthFirst<T>(this T root, Func<T, IEnumerable<T>> onElement)
		{
			Queue<T> queue = new Queue<T>();
			HashSet<T> explored = new HashSet<T>();
			explored.Add(root);
			queue.Enqueue(root);
			while (queue.Any())
			{
				T puzzle = queue.Dequeue();
				IEnumerable<T> sides = onElement(puzzle);
				foreach (T edge in sides)
				{
					if (!explored.Contains(edge))
					{
						explored.Add(edge);
						queue.Enqueue(edge);
					}
				}
			}
		}

		public static bool CheckKeyPress(this KeyCode main, params KeyCode[] mod)
		{
			return Input.GetKeyDown(main) && mod.All(Input.GetKey);
		}

		public static TimeSpan Epoch(this DateTime today)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return today.Subtract(epoch);
		}

		public static void SetStiffness(this WheelCollider wheelCollider, float mult)
		{
			WheelFrictionCurve forwardFriction = wheelCollider.forwardFriction;
			forwardFriction.stiffness = mult;
			wheelCollider.forwardFriction = forwardFriction;

			WheelFrictionCurve sidewaysFriction = wheelCollider.sidewaysFriction;
			sidewaysFriction.stiffness = mult;
			wheelCollider.sidewaysFriction = sidewaysFriction;
		}

		public static void StartColor(this ParticleSystem particleSystem, Color color)
		{
			ParticleSystem.MainModule main = particleSystem.main;
			main.startColor = color;
		}

		public static bool Is(this Type a, Type b)
		{
			return a == b || a.IsSubclassOf(b);
		}

		public static bool CompareType(this Type a, Type b)
		{
			return a == b || a.IsSubclassOf(b) || b.IsSubclassOf(a);
		}

#if TOOLS_URP
		public static T Get<T>(this VolumeProfile profile) where T : VolumeComponent
		{
			return profile.TryGet(out T component) ? component : default;
		}
#endif
	}
}
