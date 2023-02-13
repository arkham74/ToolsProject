using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace JD.TextureImporter.Editor
{
	[CustomEditor(typeof(TextureImporter))]
	internal class TextureImporterEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/Procedural Texture")]
		internal static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New procedural texture.proctex", "");
		}
	}
}