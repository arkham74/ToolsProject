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
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(sizeProp);
			EditorGUILayout.Separator();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(redChannelProp);
			EditorGUILayout.PropertyField(redTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(greenChannelProp);
			EditorGUILayout.PropertyField(greenTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(blueChannelProp);
			EditorGUILayout.PropertyField(blueTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(alphaChannelProp);
			EditorGUILayout.PropertyField(alphaTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(wrapModeProp);
			EditorGUILayout.PropertyField(filterModeProp);
			EditorGUILayout.PropertyField(anisoLevelProp);
			serializedObject.ApplyModifiedProperties();
			ApplyRevertGUI();
		}
	}
}