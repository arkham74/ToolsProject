using System.IO;
using UnityEditor;
using UnityEngine;

namespace StuntMasters.Tools.Editor
{
	public class GeneratePreview : EditorWindow
	{
		private static GameObject selected;
		private static Texture2D texture;
		private readonly GUILayoutOption layoutOption = GUILayout.Height(40);

		[MenuItem("Tools/Generate Preview")]
		private static void ShowWindow()
		{
			GetWindow<GeneratePreview>("Generate Preview");
		}

		private void OnGUI()
		{
			selected = EditorGUILayout.ObjectField("Model", selected, typeof(GameObject), false, layoutOption) as GameObject;

			if (selected)
			{
				if (GUILayout.Button("Generate Preview", layoutOption))
				{
					texture = GeneratePreviewImage(selected);
				}
			}

			if (texture) GUILayout.Box(texture, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
			// EditorGUILayout.LabelField(texture);
			// if (texture) EditorGUI.DrawTextureTransparent(new Rect(0, 50, 300, 300), texture);
		}

		private static Texture2D GeneratePreviewImage(GameObject model, int size = 256)
		{
			RuntimePreviewGenerator.OrthographicMode = true;
			RuntimePreviewGenerator.BackgroundColor = Color.clear;
			RuntimePreviewGenerator.Padding = 0.2f;
			RuntimePreviewGenerator.MarkTextureNonReadable = false;
			Texture2D tex = RuntimePreviewGenerator.GenerateModelPreview(model.transform, size, size, true);
			// Texture2D tex = AssetPreview.GetAssetPreview(model);
			SaveTextureAssetToSprite(tex, model);
			return tex;
		}

		private static void SaveTextureAssetToSprite(Texture2D tex, GameObject model)
		{
			string assetName = $"preview_{model.name.ToLower().Replace(" ", "_")}.png";
			string pathToSo = AssetDatabase.GetAssetPath(model);
			string path = Path.Combine(Path.GetDirectoryName(pathToSo), assetName);
			File.WriteAllBytes(path, tex.EncodeToPNG());
			AssetDatabase.Refresh();
			TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
			ti.isReadable = true;
			ti.spritePixelsPerUnit = 100;
			ti.mipmapEnabled = true;
			ti.alphaIsTransparency = true;
			ti.textureType = TextureImporterType.Sprite;
			EditorUtility.SetDirty(ti);
			ti.SaveAndReimport();
		}
	}
}