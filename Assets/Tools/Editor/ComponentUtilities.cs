using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Tag = NaughtyAttributes.TagAttribute;
using TMPro;

public static class ComponentUtilities
{
	[MenuItem("CONTEXT/AudioSource/Realistic Setup")]
	public static void RalisticRolloff(MenuCommand command)
	{
		Undo.RecordObject(command.context, "AudioSource Realistic Setup");
		((AudioSource)command.context).RealisticRolloff();
		command.context.MarkDirty();
	}

	public static void RealisticRolloff(this AudioSource AS)
	{
		AnimationCurve animCurve = new AnimationCurve(
				new Keyframe(AS.minDistance, 1f),
				new Keyframe(AS.minDistance + (AS.maxDistance - AS.minDistance) / 4f, .35f),
				new Keyframe(AS.maxDistance, 0f)
);

		AS.rolloffMode = AudioRolloffMode.Custom;
		animCurve.SmoothTangents(1, .025f);
		AS.SetCustomCurve(AudioSourceCurveType.CustomRolloff, animCurve);

		AS.dopplerLevel = 0f;
		AS.spread = 60f;
	}

	[MenuItem("GameObject/Create parent for each", true, 0)]
	public static bool CreateParentValid()
	{
		return Selection.gameObjects.Length > 1;
	}

	[MenuItem("GameObject/Create parent for each", false, 0)]
	public static void CreateParent(MenuCommand command)
	{
		GameObject go = command.context as GameObject;
		go.transform.SetParent(new GameObject(go.name).transform);
	}

	[MenuItem("CONTEXT/TMP_Text/Paste sample text")]
	public static void PasteSampleTMPText(MenuCommand command)
	{
		TMP_Text text = (TMP_Text)command.context;
		text.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. In neque tortor, sodales eget eleifend quis, iaculis ac sem. Sed blandit nunc vitae pulvinar tempus.");
	}

	[MenuItem("CONTEXT/Text/Paste sample text")]
	public static void PasteSampleText(MenuCommand command)
	{
		Text text = (Text)command.context;
		text.text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In neque tortor, sodales eget eleifend quis, iaculis ac sem. Sed blandit nunc vitae pulvinar tempus.";
	}

	[MenuItem("CONTEXT/Button/Copy highlight to others")]
	public static void CopyFromHighlightToOthers(MenuCommand command)
	{
		Button button = (Button)command.context;
		SpriteState spriteState;
		spriteState.highlightedSprite = button.spriteState.highlightedSprite;
		spriteState.pressedSprite = spriteState.highlightedSprite;
		spriteState.selectedSprite = spriteState.highlightedSprite;
		button.spriteState = spriteState;
	}

	[MenuItem("CONTEXT/Rigidbody/Calculate Mass")]
	private static void CalculateMass(MenuCommand command) => CalcMass(command.context as Rigidbody, 1f);

	[MenuItem("CONTEXT/Rigidbody/Calculate Mass x10")]
	private static void CalculateMass10(MenuCommand command) => CalcMass(command.context as Rigidbody, 10f);

	[MenuItem("CONTEXT/Rigidbody/Calculate Mass x100")]
	private static void CalculateMass100(MenuCommand command) => CalcMass(command.context as Rigidbody, 100f);

	private static void CalcMass(Rigidbody body, float density)
	{
		Collider col = body.GetComponentInChildren<Collider>();
		if (col)
		{
			Vector3 boundsSize = col.bounds.size;
			float volume = boundsSize.x * boundsSize.y * boundsSize.z;
			body.mass = volume * density;
		}
	}

	[MenuItem("CONTEXT/Transform/Re-Scale Parent")]
	private static void ScaleParent(MenuCommand command)
	{
		if (PrefabUtility.IsPartOfAnyPrefab(command.context))
		{
			Debug.LogWarning("DONT USE ON PREFABS");
			return;
		}

		Transform body = (Transform)command.context;

		List<Transform> children = new List<Transform>();
		while (body.childCount > 0)
		{
			Transform child = body.GetChild(0);
			child.SetParent(null);
			children.Add(child);
		}

		body.localScale = Vector3.one;
		foreach (Transform child in children)
		{
			child.SetParent(body);
		}
	}

	[MenuItem("CONTEXT/Transform/Center Parent")]
	private static void CenterParent(MenuCommand command)
	{
		if (PrefabUtility.IsPartOfAnyPrefab(command.context))
		{
			Debug.LogWarning("DONT USE ON PREFABS");
			return;
		}

		Transform body = (Transform)command.context;

		int childCount = body.childCount;
		if (childCount == 0)
			return;
		Vector3 center = Vector3.zero;

		List<Transform> children = new List<Transform>();
		while (body.childCount > 0)
		{
			Transform child = body.GetChild(0);
			children.Add(child);
			center += child.position;
			child.SetParent(null);
		}

		center /= childCount;
		body.position = center;
		foreach (Transform child in children)
		{
			child.SetParent(body);
		}
	}

	[MenuItem("CONTEXT/Transform/Zero Parent")]
	private static void ZeroParent(MenuCommand command)
	{
		if (PrefabUtility.IsPartOfAnyPrefab(command.context))
		{
			Debug.LogWarning("DONT USE ON PREFABS");
			return;
		}

		Transform body = (Transform)command.context;

		List<Transform> children = new List<Transform>();
		while (body.childCount > 0)
		{
			Transform child = body.GetChild(0);
			children.Add(child);
			child.SetParent(null);
		}

		body.position = Vector3.zero;
		foreach (Transform child in children)
		{
			child.SetParent(body);
		}
	}
}