#if TOOLS_LOCALIZATION
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
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace JD.Editor
{
	public static class TextMeshProUtilities
	{
		[MenuItem("CONTEXT/Text/Convert to TextMeshPro")]
		static void ConvertToTextMeshPro(MenuCommand command)
		{
			Text oldText = command.context as Text;
			GameObject go = oldText.gameObject;
			string txt = oldText.text;
			FontStyle style = oldText.fontStyle;
			TextAnchor aligment = oldText.alignment;
			bool aliGeo = oldText.alignByGeometry;
			bool rich = oldText.supportRichText;
			Color color = oldText.color;
			int size = oldText.fontSize;
			HorizontalWrapMode horizontalWrapMode = oldText.horizontalOverflow;
			VerticalWrapMode verticalWrapMode = oldText.verticalOverflow;
			Object.DestroyImmediate(oldText);

			if (!go.TryGetComponent(out TextMeshProUGUI text))
			{
				text = Undo.AddComponent<TextMeshProUGUI>(go);
			}

			text.fontStyle = (FontStyles)style;
			text.text = txt;
			text.richText = rich;
			text.alignment = ConvertAligment(aligment, aliGeo);
			text.enableWordWrapping = horizontalWrapMode == HorizontalWrapMode.Wrap;
			text.overflowMode = ConvertOverflow(verticalWrapMode);
			text.color = color;
			text.fontSize = size;
			if (!go.name.Contains("(TMP)"))
			{
				go.name += " (TMP)";
			}
		}

		private static TextOverflowModes ConvertOverflow(VerticalWrapMode verticalWrapMode) => verticalWrapMode switch
		{
			VerticalWrapMode.Overflow => TextOverflowModes.Overflow,
			VerticalWrapMode.Truncate => TextOverflowModes.Truncate,
			_ => TextOverflowModes.Truncate,
		};

		private static TextAlignmentOptions ConvertAligment(TextAnchor aligment, bool geo) => (aligment, geo) switch
		{
			(TextAnchor.UpperLeft, false) => TextAlignmentOptions.TopLeft,
			(TextAnchor.UpperCenter, false) => TextAlignmentOptions.Top,
			(TextAnchor.UpperRight, false) => TextAlignmentOptions.TopRight,

			(TextAnchor.MiddleLeft, false) => TextAlignmentOptions.Left,
			(TextAnchor.MiddleCenter, false) => TextAlignmentOptions.Center,
			(TextAnchor.MiddleRight, false) => TextAlignmentOptions.Right,

			(TextAnchor.LowerLeft, false) => TextAlignmentOptions.BottomLeft,
			(TextAnchor.LowerCenter, false) => TextAlignmentOptions.Bottom,
			(TextAnchor.LowerRight, false) => TextAlignmentOptions.BottomRight,

			(TextAnchor.UpperCenter, true) => TextAlignmentOptions.TopGeoAligned,
			(TextAnchor.MiddleCenter, true) => TextAlignmentOptions.CenterGeoAligned,
			(TextAnchor.LowerCenter, true) => TextAlignmentOptions.BottomGeoAligned,
			_ => TextAlignmentOptions.Center,
		};

		[MenuItem("CONTEXT/TextMeshProUGUI/Localize Text Mesh Pro")]
		static void LocalizeTMProText(MenuCommand command)
		{
			SetupForLocalization(command.context as TextMeshProUGUI);
		}

		public static void SetupForLocalization(TextMeshProUGUI target)
		{
			if (target.text.Length > 1)
			{
				if (!target.gameObject.TryGetComponent(out LocalizeStringEvent comp))
				{
					comp = Undo.AddComponent<LocalizeStringEvent>(target.gameObject);
				}

				MethodInfo setStringMethod = target.GetType().GetProperty("text").GetSetMethod();
				UnityAction<string> methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<string>), target, setStringMethod) as UnityAction<string>;
				UnityEventTools.AddPersistentListener(comp.OnUpdateString, methodDelegate);
				comp.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
				string key = target.text.ToConstantCase();

				if (key.IsNullOrWhiteSpaceOrEmpty())
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
				comp.StringReference.WaitForCompletion = true;
				comp.StringReference.Add("variable", new IntVariable());
				comp.MarkDirty();
			}
		}
	}
}
#endif