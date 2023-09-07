using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace JD.SDFImporter.Editor
{
	[CustomEditor(typeof(SDFImporter))]
	internal class SDFImporterEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/SDF Texture")]
		internal static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New SDF texture.sdf", "");
		}
	}
}