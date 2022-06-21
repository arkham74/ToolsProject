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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ButtonMultiGraphics : Button
{
	[SerializeField] private Graphic[] targets = new Graphic[0];

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);

		if (!gameObject.activeInHierarchy)
			return;

		Color tintColor;
		Sprite transitionSprite;
		// string triggerName;

		switch (state)
		{
			case SelectionState.Normal:
				tintColor = colors.normalColor;
				transitionSprite = null;
				// triggerName = animationTriggers.normalTrigger;
				break;
			case SelectionState.Highlighted:
				tintColor = colors.highlightedColor;
				transitionSprite = spriteState.highlightedSprite;
				// triggerName = animationTriggers.highlightedTrigger;
				break;
			case SelectionState.Pressed:
				tintColor = colors.pressedColor;
				transitionSprite = spriteState.pressedSprite;
				// triggerName = animationTriggers.pressedTrigger;
				break;
			case SelectionState.Selected:
				tintColor = colors.selectedColor;
				transitionSprite = spriteState.selectedSprite;
				// triggerName = animationTriggers.selectedTrigger;
				break;
			case SelectionState.Disabled:
				tintColor = colors.disabledColor;
				transitionSprite = spriteState.disabledSprite;
				// triggerName = animationTriggers.disabledTrigger;
				break;
			default:
				tintColor = Color.black;
				transitionSprite = null;
				// triggerName = string.Empty;
				break;
		}

		switch (transition)
		{
			case Transition.ColorTint:
				StartColorTween(tintColor * colors.colorMultiplier, instant);
				break;
			case Transition.SpriteSwap:
				DoSpriteSwap(transitionSprite);
				break;
		}

		void StartColorTween(Color targetColor, bool instant)
		{
			foreach (Graphic graph in targets)
			{
				if (graph == null) continue;
				graph.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
			}
		}

		void DoSpriteSwap(Sprite newSprite)
		{
			foreach (Graphic graph in targets)
			{
				if (graph is not Image img) continue;
				img.overrideSprite = newSprite;
			}
		}
	}
}