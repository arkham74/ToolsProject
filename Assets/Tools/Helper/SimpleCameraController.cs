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
using NaughtyAttributes;
using UnityEditor;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SimpleCameraController : MonoBehaviour
{
#if !ENABLE_INPUT_SYSTEM
	[BoxGroup("Move"), InputAxis] public string xAxis;
	[BoxGroup("Move"), InputAxis] public string yAxis;
	[BoxGroup("Move"), InputAxis] public string zAxis;
	private Vector2 mousePos = Vector2.zero;
#endif

	[BoxGroup("Move")] public float moveSmoothFactor = 0.9f;
	[BoxGroup("Move")] public float moveMaxSpeed = 10f;
	[BoxGroup("Move")] public Vector3 moveSpeed = Vector3.zero;
	[BoxGroup("Look")] public float lookMaxSpeed = 10f;
	private Vector2 lookDelta = Vector3.zero;

#if UNITY_EDITOR
	private void OnEnable()
	{
		Transform trans = transform;
		Vector3 position = trans.position;
		position.x = EditorPrefs.GetFloat("pos_x", position.x);
		position.y = EditorPrefs.GetFloat("pos_y", position.y);
		position.z = EditorPrefs.GetFloat("pos_z", position.z);
		trans.position = position;
		Vector3 euler = trans.rotation.eulerAngles;
		euler.x = EditorPrefs.GetFloat("euler_x", euler.x);
		euler.y = EditorPrefs.GetFloat("euler_y", euler.y);
		euler.z = EditorPrefs.GetFloat("euler_z", euler.z);
		trans.rotation = Quaternion.Euler(euler);
		lookDelta = euler;
	}

	private void OnDisable()
	{
		Transform trans = transform;
		Vector3 position = trans.position;
		EditorPrefs.SetFloat("pos_x", position.x);
		EditorPrefs.SetFloat("pos_y", position.y);
		EditorPrefs.SetFloat("pos_z", position.z);
		Vector3 euler = trans.rotation.eulerAngles;
		EditorPrefs.SetFloat("euler_x", euler.x);
		EditorPrefs.SetFloat("euler_y", euler.y);
		EditorPrefs.SetFloat("euler_z", euler.z);
	}

	[Button]
	private void Reset()
	{
		OnEnable();
	}
#endif

#if !ENABLE_INPUT_SYSTEM
	private void Update()
	{
		mousePos = Input.mousePosition;
	}
#endif

	private void LateUpdate()
	{
		MoveUpdate();
#if ENABLE_INPUT_SYSTEM
		Vector2 delta = Mouse.current.delta.ReadValue().InvertY().XYtoYX();
		LookUpdate(delta);
#else
		LookUpdate(mousePos - (Vector2)Input.mousePosition);
#endif
	}

	private void LookUpdate(Vector2 delta)
	{
#if ENABLE_INPUT_SYSTEM
		bool isPressed = Mouse.current.rightButton.isPressed;
#else
		bool isPressed = Input.GetMouseButton(1);
#endif

		Cursor.lockState = isPressed ? CursorLockMode.Locked : CursorLockMode.None;
		if (isPressed)
		{
			if (delta != Vector2.zero)
			{
				lookDelta += delta * (lookMaxSpeed * Time.deltaTime);
			}

			lookDelta.x = Mathf.Clamp(lookDelta.x, -90, 90);
			transform.rotation = Quaternion.Euler(lookDelta);
		}
	}

	private void MoveUpdate()
	{
		Vector3 delta = Get3DAxis();
		if (delta != Vector3.zero)
		{
			moveSpeed = delta * moveMaxSpeed;
		}

		transform.Translate(moveSpeed * Time.deltaTime, Space.Self);
		moveSpeed *= moveSmoothFactor;
	}

#if ENABLE_INPUT_SYSTEM
	private Vector3 Get3DAxis()
	{
		float eKey = Convert.ToSingle(Keyboard.current.eKey.isPressed);
		float qKey = -Convert.ToSingle(Keyboard.current.qKey.isPressed);

		float wKey = Convert.ToSingle(Keyboard.current.wKey.isPressed);
		float sKey = -Convert.ToSingle(Keyboard.current.sKey.isPressed);

		float aKey = -Convert.ToSingle(Keyboard.current.aKey.isPressed);
		float dKey = Convert.ToSingle(Keyboard.current.dKey.isPressed);

		return new Vector3(aKey + dKey, eKey + qKey, wKey + sKey);
	}
#else
	private Vector3 Get3DAxis()
	{
		float x = Input.GetAxis(xAxis);
		float y = Input.GetAxis(yAxis);
		float z = Input.GetAxis(zAxis);
		return new Vector3(x, y, z);
	}
#endif
}