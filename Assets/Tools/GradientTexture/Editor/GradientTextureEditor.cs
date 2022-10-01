using UnityEditor;
using UnityEditor.AssetImporters;

namespace JD.GradientTexture.Editor
{
	public class GradientTextureEditor : ScriptedImporterEditor
	{
		[MenuItem("Assets/Create/Gradient Texture")]
		public static void CreateNewAsset()
		{
			ProjectWindowUtil.CreateAssetWithContent("New Gradient Texture.gradient_texture", "");
		}
	}
}