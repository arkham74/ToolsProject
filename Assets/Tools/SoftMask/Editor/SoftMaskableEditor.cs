using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using MaskIntr = UnityEngine.SpriteMaskInteraction;

namespace Coffee.UISoftMask
{
	internal enum MaskInteraction : int
	{
		VisibleInsideMask = (1 << 0) + (1 << 2) + (1 << 4) + (1 << 6),
		VisibleOutsideMask = (2 << 0) + (2 << 2) + (2 << 4) + (2 << 6),
		Custom = -1,
	}

	/// <summary>
	/// SoftMaskable editor.
	/// </summary>
	[CustomEditor(typeof(SoftMaskable))]
	[CanEditMultipleObjects]
	public class SoftMaskableEditor : Editor
	{
		private static readonly List<Mask> s_TmpMasks = new List<Mask>();
		private static GUIContent s_MaskWarning;
		private SerializedProperty _spMaskInteraction;
		private bool _custom;

		private MaskInteraction MaskInteraction
		{
			get
			{
				int value = _spMaskInteraction.intValue;
				return _custom
					 ? MaskInteraction.Custom
					 : System.Enum.IsDefined(typeof(MaskInteraction), value)
						  ? (MaskInteraction)value
						  : MaskInteraction.Custom;
			}
			set
			{
				_custom = (value == MaskInteraction.Custom);
				if (!_custom)
				{
					_spMaskInteraction.intValue = (int)value;
				}
			}
		}


		private void OnEnable()
		{
			_spMaskInteraction = serializedObject.FindProperty("m_MaskInteraction");
			_custom = (MaskInteraction == MaskInteraction.Custom);

			if (s_MaskWarning == null)
			{
				s_MaskWarning = new GUIContent(EditorGUIUtility.FindTexture("console.warnicon.sml"), "This is not a SoftMask component.");
			}
		}

		private void DrawMaskInteractions()
		{
			SoftMaskable softMaskable = target as SoftMaskable;
			if (softMaskable == null)
			{
				return;
			}

			softMaskable.GetComponentsInParent(true, s_TmpMasks);
			s_TmpMasks.RemoveAll(x => !x.enabled);
			s_TmpMasks.Reverse();

			MaskInteraction = (MaskInteraction)EditorGUILayout.EnumPopup("Mask Interaction", MaskInteraction);
			if (!_custom)
			{
				return;
			}

			float l = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 45;

			using (EditorGUI.ChangeCheckScope ccs = new EditorGUI.ChangeCheckScope())
			{
				int intr0 = DrawMaskInteraction(0);
				int intr1 = DrawMaskInteraction(1);
				int intr2 = DrawMaskInteraction(2);
				int intr3 = DrawMaskInteraction(3);

				if (ccs.changed)
				{
					_spMaskInteraction.intValue = (intr0 << 0) + (intr1 << 2) + (intr2 << 4) + (intr3 << 6);
				}
			}

			EditorGUIUtility.labelWidth = l;
		}

		private int DrawMaskInteraction(int layer)
		{
			Mask mask = layer < s_TmpMasks.Count ? s_TmpMasks[layer] : null;
			MaskIntr intr = (MaskIntr)((_spMaskInteraction.intValue >> layer * 2) & 0x3);
			if (!mask)
			{
				return (int)intr;
			}

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(mask is SoftMask ? GUIContent.none : s_MaskWarning, GUILayout.Width(16));
			GUILayout.Space(-5);
			EditorGUILayout.ObjectField("Mask " + layer, mask, typeof(Mask), false);
			GUILayout.Space(-15);
			intr = (MaskIntr)EditorGUILayout.EnumPopup(intr);
			GUILayout.EndHorizontal();

			return (int)intr;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			serializedObject.Update();
			DrawMaskInteractions();

			serializedObject.ApplyModifiedProperties();

			SoftMaskable current = target as SoftMaskable;
			if (current == null)
			{
				return;
			}

			SoftMask mask = current.SoftMask;
			if (mask)
			{
				return;
			}

			GUILayout.BeginHorizontal();
			EditorGUILayout.HelpBox("This is unnecessary SoftMaskable.\nCan't find any SoftMask components above.", MessageType.Warning);
			if (GUILayout.Button("Remove", GUILayout.Height(40)))
			{
				DestroyImmediate(current);
				EditorUtils.MarkPrefabDirty();
			}

			GUILayout.EndHorizontal();
		}
	}
}