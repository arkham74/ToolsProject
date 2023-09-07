using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace JD.VoronoiImporter.Editor
{
	[CustomEditor(typeof(VoronoiImporter))]
	public class VoronoiImporterEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/Voronoi Texture")]
		public static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New Voronoi texture.voronoi", "");
		}
	}
}