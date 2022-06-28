using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
		if (a.Equals(b))
			return true;

		if (a.IsSubclassOf(b))
			return true;

		if (b.IsSubclassOf(a))
			return true;

		return false;
	}

	public static T Get<T>(this VolumeProfile profile) where T : VolumeComponent
	{
		return profile.TryGet(out T component) ? component : default;
	}
}