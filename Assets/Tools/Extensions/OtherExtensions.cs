using System;
using System.Linq;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace JD
{
	public static class OtherExtensions
	{
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

		public static void Register(this Toggle toggle, UnityAction<bool> func)
		{
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(func);
		}

		public static void Register(this Dropdown dropdown, UnityAction<int> func)
		{
			dropdown.onValueChanged.RemoveAllListeners();
			dropdown.onValueChanged.AddListener(func);
		}

		public static void StartColor(this ParticleSystem particleSystem, Color color)
		{
			ParticleSystem.MainModule main = particleSystem.main;
			main.startColor = color;
		}

		public static bool CompareType(this Type a, Type b)
		{
			return a == b || a.IsSubclassOf(b) || b.IsSubclassOf(a);
		}

		public static T Get<T>(this VolumeProfile profile) where T : VolumeComponent
		{
			return profile.TryGet(out T component) ? component : default;
		}
	}
}
