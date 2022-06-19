#if TOOLS_LOCAL
using System;
using System.Reflection;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

using UnityEditor;
using UnityEditor.Events;
using UnityEditor.Localization;

using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using System.Collections.Generic;

public static class TextMeshProUGUIUtilities
{
	[MenuItem("CONTEXT/TextMeshProUGUI/Localize Text Mesh Pro")]
	static void LocalizeTMProText(MenuCommand command)
	{
		SetupForLocalization(command.context as TextMeshProUGUI);
	}

	public static void SetupForLocalization(TextMeshProUGUI target)
	{
		if (target.text.Length > 1)
		{
			LocalizeStringEvent comp = Undo.AddComponent<LocalizeStringEvent>(target.gameObject);
			MethodInfo setStringMethod = target.GetType().GetProperty("text").GetSetMethod();
			UnityAction<string> methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<string>), target, setStringMethod) as UnityAction<string>;
			UnityEventTools.AddPersistentListener(comp.OnUpdateString, methodDelegate);
			comp.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
			string key = target.text.ToConstantCase();

			if (key.IsNullOrWhiteSpace())
			{
				Debug.LogWarning("Is white space", target);
			}

			StringTableCollection stringTableColl = LocalizationEditorSettings.GetStringTableCollections()[0];
			StringTable stringTable = stringTableColl.StringTables[0];
			StringTableEntry entry = stringTable.AddEntry(key, target.text);
			stringTableColl.MarkDirty();
			stringTableColl.SharedData.MarkDirty();
			stringTable.MarkDirty();
			comp.StringReference = new LocalizedString(entry.Table.TableCollectionName, entry.KeyId);
			comp.MarkDirty();
		}
	}
}
#endif