using UnityEditor;
using UnityEditor.AssetImporters;

namespace Texture3DGenerator.Editor
{
	[CustomEditor(typeof(Texture3DGenerator))]
	public class Texture3DGeneratorEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/Noise Texture 3D")]
		public static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New noise texture3d.tex3d", "");
		}
	}
}