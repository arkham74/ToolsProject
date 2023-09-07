using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace JD.VoronoiImporter.Editor
{
	[CustomEditor(typeof(VoronoiImporter))]
	internal class VoronoiImporterEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/Voronoi Texture")]
		internal static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New Voronoi texture.voronoi", "");
		}
	}
}