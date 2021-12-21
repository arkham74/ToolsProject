using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace TextureChannelPacker.Editor
{
	[CustomEditor(typeof(TextureChannelPacker))]
	public class TextureChannelPackerEditor : ScriptedImporterEditor
	{
		private SerializedProperty sizeProp;
		private SerializedProperty redTexProp;
		private SerializedProperty greenTexProp;
		private SerializedProperty blueTexProp;
		private SerializedProperty alphaTexProp;
		private SerializedProperty redChannelProp;
		private SerializedProperty greenChannelProp;
		private SerializedProperty blueChannelProp;
		private SerializedProperty alphaChannelProp;
		private SerializedProperty wrapModeProp;
		private SerializedProperty filterModeProp;
		private SerializedProperty anisoLevelProp;
		private SerializedProperty redInvertProp;
		private SerializedProperty greenInvertProp;
		private SerializedProperty blueInvertProp;
		private SerializedProperty alphaInvertProp;

		private readonly GUIContent redContent = new GUIContent("Red");
		private readonly GUIContent greenContent = new GUIContent("Green");
		private readonly GUIContent blueContent = new GUIContent("Blue");
		private readonly GUIContent alphaContent = new GUIContent("Alpha");
		private readonly GUILayoutOption width = GUILayout.Width(100);
		private readonly GUILayoutOption height = GUILayout.Height(100);

		[MenuItem("Assets/Create/Texture Packer")]
		public static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New texture packer.texpack", "");
		}

		public override void OnEnable()
		{
			base.OnEnable();
			sizeProp = serializedObject.FindProperty("size");
			redTexProp = serializedObject.FindProperty("redTex");
			greenTexProp = serializedObject.FindProperty("greenTex");
			blueTexProp = serializedObject.FindProperty("blueTex");
			alphaTexProp = serializedObject.FindProperty("alphaTex");
			redChannelProp = serializedObject.FindProperty("redChannel");
			greenChannelProp = serializedObject.FindProperty("greenChannel");
			blueChannelProp = serializedObject.FindProperty("blueChannel");
			alphaChannelProp = serializedObject.FindProperty("alphaChannel");
			wrapModeProp = serializedObject.FindProperty("wrapMode");
			filterModeProp = serializedObject.FindProperty("filterMode");
			anisoLevelProp = serializedObject.FindProperty("anisoLevel");
			redInvertProp = serializedObject.FindProperty("redInvert");
			greenInvertProp = serializedObject.FindProperty("greenInvert");
			blueInvertProp = serializedObject.FindProperty("blueInvert");
			alphaInvertProp = serializedObject.FindProperty("alphaInvert");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(sizeProp);
			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(redInvertProp, redContent);
			EditorGUILayout.PropertyField(redChannelProp, GUIContent.none);
			EditorGUILayout.PropertyField(redTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(greenInvertProp, greenContent);
			EditorGUILayout.PropertyField(greenChannelProp, GUIContent.none);
			EditorGUILayout.PropertyField(greenTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(blueInvertProp, blueContent);
			EditorGUILayout.PropertyField(blueChannelProp, GUIContent.none);
			EditorGUILayout.PropertyField(blueTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(alphaInvertProp, alphaContent);
			EditorGUILayout.PropertyField(alphaChannelProp, GUIContent.none);
			EditorGUILayout.PropertyField(alphaTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(wrapModeProp);
			EditorGUILayout.PropertyField(filterModeProp);
			EditorGUILayout.PropertyField(anisoLevelProp);
			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal();
			GUIContent redPrev = new GUIContent(redTexProp.objectReferenceValue as Texture2D);
			GUIContent greenPrev = new GUIContent(greenTexProp.objectReferenceValue as Texture2D);
			GUIContent bluePrev = new GUIContent(blueTexProp.objectReferenceValue as Texture2D);
			GUIContent alphaPrev = new GUIContent(alphaTexProp.objectReferenceValue as Texture2D);
			EditorGUILayout.LabelField(redPrev, width, height);
			EditorGUILayout.LabelField(greenPrev, width, height);
			EditorGUILayout.LabelField(bluePrev, width, height);
			EditorGUILayout.LabelField(alphaPrev, width, height);
			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
			ApplyRevertGUI();
		}
	}
}