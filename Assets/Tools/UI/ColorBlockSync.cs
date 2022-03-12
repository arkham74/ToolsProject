using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[ExecuteAlways]
public class ColorBlockSync : MonoBehaviour
{
	public Selectable selectable;
	[Expandable] public ColorBlockData data;

#if UNITY_EDITOR
	private void Reset()
	{
		selectable = GetComponent<Selectable>();
		data = AssetTools.FindAssetByType<ColorBlockData>();
	}

	private void Update()
	{
		selectable.colors = data.colorBlock;
	}
#endif
}