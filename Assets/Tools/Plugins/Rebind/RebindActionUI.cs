#if ENABLE_INPUT_SYSTEM
using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
#if TOOLS_LOCALIZATION
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
#endif

[RequireComponent(typeof(Button))]
public class RebindActionUI : MonoBehaviour
{
	[OnValueChanged(nameof(OnChange))][SerializeField] private bool useFallback;
#if TOOLS_LOCALIZATION
	[OnValueChanged(nameof(OnChange))][SerializeField] private string tableKey = "Game";
#endif
	[OnValueChanged(nameof(OnChange))][SerializeField] private Button button;
	[OnValueChanged(nameof(OnChange))][SerializeField] private string actionFormat = "{0}";
	[OnValueChanged(nameof(OnChange))][SerializeField] private string bindingFormat = "{0}";
	[OnValueChanged(nameof(OnChange))][SerializeField] private string waitInfo = "UI_CONTROLS_WAIT";
#if !TOOLS_LOCALIZATION
	[OnValueChanged(nameof(OnChange))][SerializeField] private InputBinding.DisplayStringOptions displayOptions = InputBinding.DisplayStringOptions.DontIncludeInteractions | InputBinding.DisplayStringOptions.DontUseShortDisplayNames;
#endif
	[OnValueChanged(nameof(OnChange))][SerializeField] private InputActionReference inputActionReference;
	[OnValueChanged(nameof(OnChange))][SerializeField][Binding(nameof(inputActionReference))] private string bindingId;

	[OnValueChanged(nameof(OnChange))][SerializeField] private Text actionText;
	[OnValueChanged(nameof(OnChange))][SerializeField] private Text bindingText;

	public static Action<RebindActionUI> OnRebind = delegate { };
	public static Action<RebindActionUI> OnRebindFail = delegate { };
	public static Action OnUpdateBindingDisplay = delegate { };
	private InputActionRebindingExtensions.RebindingOperation rebindOperation;

	public static void UpdateBindingDisplays()
	{
		OnUpdateBindingDisplay.Invoke();
	}

	private void OnChange()
	{
#if UNITY_EDITOR
		if (!EditorApplication.isPlayingOrWillChangePlaymode)
		{
			Refresh();
		}
#endif
	}

	private void Reset()
	{
		button = GetComponent<Button>();
	}

	private void Awake()
	{
		button.Register(Button_Rebind);
		OnUpdateBindingDisplay += UpdateBindingDisplay;
#if TOOLS_LOCALIZATION
		LocalizationSettings.SelectedLocaleChanged += LocaleChanged;
#endif
		Refresh();
	}

	private void OnDestroy()
	{
		OnUpdateBindingDisplay -= UpdateBindingDisplay;
#if TOOLS_LOCALIZATION
		LocalizationSettings.SelectedLocaleChanged -= LocaleChanged;
#endif
		rebindOperation?.Dispose();
		rebindOperation = null;
	}

#if TOOLS_LOCALIZATION
	private void LocaleChanged(Locale obj)
	{
		Refresh();
	}
#endif

	private void Refresh()
	{
		UpdateActionDisplay();
		UpdateBindingDisplay();
	}

	private void Button_Rebind()
	{
		rebindOperation?.Cancel(); // Will null out m_RebindOperation.

		void CleanUp()
		{
			rebindOperation?.Dispose();
			rebindOperation = null;
		}

		InputAction action = inputActionReference.action;
		int bindingIndex = action.GetBindingIndexByID(bindingId);
		string path = action.bindings[bindingIndex].path;

		rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
			.WithCancelingThrough("*/{Cancel}")
			.WithMagnitudeHavingToBeGreaterThan(0.5f)
			.WithControlsExcluding("<Keyboard>/printScreen")
			.WithControlsExcluding("<Keyboard>/anyKey")
			.WithControlsExcluding("<Gamepad>/select")
			.OnCancel(ope =>
			{
				UpdateBindingDisplay();
				CleanUp();
			})
			.OnComplete(ope =>
			{
				UpdateBindingDisplay();
				CleanUp();
				if (CheckDuplicate(action, bindingIndex))
				{
					action.ApplyBindingOverride(bindingIndex, path);
					Button_Rebind();
					OnRebindFail.Invoke(this);
				}
				else
				{
					OnRebind.Invoke(this);
				}
			});

#if TOOLS_LOCALIZATION
		bindingText.SetLocalizedText(tableKey, waitInfo, bindingFormat);
#else
		bindingText.SetText(string.Format(bindingFormat, waitInfo));
#endif
		rebindOperation.Start();
	}

	private static bool CheckDuplicate(InputAction action, int bindingIndex)
	{
		InputBinding binding = action.bindings[bindingIndex];

		foreach (InputBinding inputBinding in action.actionMap.bindings)
		{
			if (Compare(inputBinding, binding)) continue;
			if (inputBinding.effectivePath == binding.effectivePath) return true;
		}

		return false;
	}

	private static bool Compare(InputBinding inputBinding, InputBinding binding)
	{
		string name1 = inputBinding.isPartOfComposite ? inputBinding.name : inputBinding.action;
		string name2 = binding.isPartOfComposite ? binding.name : binding.action;
		return name1 == name2;
	}

	public void UpdateActionDisplay()
	{
		InputAction action = inputActionReference.action;
		InputBinding binding = action.GetBindingByID(bindingId);
		string display = binding.isPartOfComposite ? binding.name : action.name;
		SetText(actionText, actionFormat, display, "ACTION");
	}

	public void UpdateBindingDisplay()
	{
		InputAction action = inputActionReference.action;
		int index = action.GetBindingIndexByID(bindingId);
#if TOOLS_LOCALIZATION
		string display = action.GetBindingDisplayString(index, InputBinding.DisplayStringOptions.DontIncludeInteractions);
#else
		string display = action.GetBindingDisplayString(index, displayOptions);
#endif
		SetText(bindingText, bindingFormat, display, "BINDING");
	}

	private void SetText(Text text, string format, string display, string prefix)
	{
#if TOOLS_LOCALIZATION
		string key = $"{prefix}_{display.ToConstantCase()}";
		if (Application.isPlaying)
		{
			if (useFallback)
				text.SetLocalizedTextFallback(tableKey, key, display, format);
			else
				text.SetLocalizedText(tableKey, key, format);
		}
		else
#endif
			text.SetText(string.Format(format, display));
	}
}
#endif