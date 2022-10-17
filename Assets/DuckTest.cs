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
using UnityEngine.Assertions.Must;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class DuckTest : MonoBehaviour
{
	private void Awake()
	{
		Test();
	}

	public void Test()
	{
		foreach (int i in ..10)
		{
			Debug.LogWarning(i);
		}
	}
}