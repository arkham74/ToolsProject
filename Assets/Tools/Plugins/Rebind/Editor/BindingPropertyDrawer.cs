#if ENABLE_INPUT_SYSTEM
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[CustomPropertyDrawer(typeof(BindingAttribute))]
public class BindingPropertyDrawer : PropertyDrawer
{
	private GUIContent[] bindingOptions;
	private string[] bindingOptionValues;
	private int selectedBindingOption;

	public override void OnGUI(Rect position, SerializedProperty bindingIdProperty, GUIContent bindingLabel)
	{
		RefreshBindingOptions(bindingIdProperty);

		if (bindingOptions != null)
		{
			int newSelectedBinding = EditorGUI.Popup(position, bindingLabel, selectedBindingOption, bindingOptions);
			if (newSelectedBinding != selectedBindingOption)
			{
				string bindingId = bindingOptionValues[newSelectedBinding];
				bindingIdProperty.stringValue = bindingId;
				selectedBindingOption = newSelectedBinding;
			}
		}
		else
		{
			EditorGUI.PropertyField(position, bindingIdProperty, bindingLabel);
		}
	}

	private void RefreshBindingOptions(SerializedProperty bindingIdProperty)
	{
		BindingAttribute attr = attribute as BindingAttribute;
		SerializedProperty actionProp = bindingIdProperty.serializedObject.FindProperty(attr.inputReference);
		InputActionReference actionReference = (InputActionReference) actionProp.objectReferenceValue;

		if (actionReference != null)
		{
			InputAction action = actionReference.action;

			if (action == null)
			{
				bindingOptions = new GUIContent[0];
				bindingOptionValues = new string[0];
				selectedBindingOption = -1;
				return;
			}

			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int bindingCount = bindings.Count;

			bindingOptions = new GUIContent[bindingCount];
			bindingOptionValues = new string[bindingCount];
			selectedBindingOption = -1;

			string currentBindingId = bindingIdProperty.stringValue;
			for (int i = 0; i < bindingCount; ++i)
			{
				InputBinding binding = bindings[i];
				string bindingId = binding.id.ToString();
				bool haveBindingGroups = !string.IsNullOrEmpty(binding.groups);

				// If we don't have a binding groups (control schemes), show the device that if there are, for example,
				// there are two bindings with the display string "A", the user can see that one is for the keyboard
				// and the other for the gamepad.
				InputBinding.DisplayStringOptions displayOptions =
					InputBinding.DisplayStringOptions.DontUseShortDisplayNames |
					InputBinding.DisplayStringOptions.IgnoreBindingOverrides;
				if (!haveBindingGroups)
				{
					displayOptions |= InputBinding.DisplayStringOptions.DontOmitDevice;
				}

				// Create display string.
				string displayString = action.GetBindingDisplayString(i, displayOptions);

				// If binding is part of a composite, include the part name.
				if (binding.isPartOfComposite)
				{
					displayString = $"{ObjectNames.NicifyVariableName(binding.name)}: {displayString}";
				}

				// Some composites use '/' as a separator. When used in popup, this will lead to to submenus. Prevent
				// by instead using a backlash.
				displayString = displayString.Replace('/', '\\');

				// If the binding is part of control schemes, mention them.
				if (haveBindingGroups)
				{
					InputActionAsset asset = action.actionMap?.asset;
					if (asset != null)
					{
						string controlSchemes = string.Join(", ",
							binding.groups.Split(InputBinding.Separator)
								.Select(x => asset.controlSchemes.FirstOrDefault(c => c.bindingGroup == x).name));

						displayString = $"{displayString} ({controlSchemes})";
					}
				}

				bindingOptions[i] = new GUIContent(displayString);
				bindingOptionValues[i] = bindingId;

				if (currentBindingId == bindingId)
				{
					selectedBindingOption = i;
				}
			}
		}
	}
}
#endif
