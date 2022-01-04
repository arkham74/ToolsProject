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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
	public Color32 textColor = Color.red;
	public Color32 hoverColor = Color.blue;
	public Text text;

	private void Reset()
	{
		text = GetComponent<Text>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int linkIndex = GetIndex(eventData.pressPosition);

		// was a link clicked?
		if (linkIndex != -1)
		{
			TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];

			string url = linkInfo.GetLinkID();
			// Debug.LogWarning(url);

			// open the link id as a url, which is the metadata we added in the text field
			Application.OpenURL(url);
		}
	}

	private int GetIndex(Vector2 mousePosition)
	{
		return TMP_TextUtilities.FindIntersectingLink(text, mousePosition, null);
	}

	public void Update()
	{
		for (int i = 0; i < text.textInfo.linkInfo.Length; i++)
		{
			SetLinkToColor(i, textColor);
		}
	}

	private void LateUpdate()
	{
		int linkIndex = GetIndex(Mouse.current.position.ReadValue());
		if (linkIndex != -1)
		{
			SetLinkToColor(linkIndex, hoverColor);
		}
	}

	public void SetLinkToColor(int linkIndex, Color32 color)
	{
		TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];

		for (int i = 0; i < linkInfo.linkTextLength; i++)
		{ // for each character in the link string
			int characterIndex = linkInfo.linkTextfirstCharacterIndex + i; // the character index into the entire text
			var charInfo = text.textInfo.characterInfo[characterIndex];
			int meshIndex = charInfo.materialReferenceIndex; // Get the index of the material / sub text object used by this character.
			int vertexIndex = charInfo.vertexIndex; // Get the index of the first vertex of this character.

			Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32; // the colors for this character

			if (charInfo.isVisible)
			{
				vertexColors[vertexIndex + 0] = color;
				vertexColors[vertexIndex + 1] = color;
				vertexColors[vertexIndex + 2] = color;
				vertexColors[vertexIndex + 3] = color;
			}
		}

		// Update Geometry
		text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
	}
}