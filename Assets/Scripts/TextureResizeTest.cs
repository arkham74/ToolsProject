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

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class TextureResizeTest : MonoBehaviour
{
	[SerializeField] private FilterMode filterMode = FilterMode.Bilinear;
	[SerializeField] private int width = 64;
	[SerializeField] private int height = 64;
	[SerializeField] private RawImage input;
	[SerializeField] private RawImage output;

	private void OnValidate()
	{
		Resize();
	}

	private void Resize()
	{
		Texture2D texture = (Texture2D)input.texture;
		if (filterMode == FilterMode.Point)
		{
			texture = texture.ResizePoint(width, height);
		}
		else
		{
			texture = texture.ResizeBilinear(width, height);
		}
		texture.filterMode = FilterMode.Point;
		output.texture = texture;
	}
}