using UnityEditor;
using UnityEditor.PackageManager;

namespace JD.Editor
{
	[InitializeOnLoad]
	public class ImportPackages
	{
		static ImportPackages()
		{
#if !TOOLS_TOOLBAR
			Client.Add("https://github.com/marijnz/unity-toolbar-extender.git");
#endif
#if !TOOLS_FREYA
			Client.Add("https://github.com/FreyaHolmer/Mathfs.git");
#endif
#if !TOOLS_COMP_VIS
			Client.Add("com.needle.compilation-visualizer");
#endif
#if !TOOLS_RENAMER
			Client.Add("https://github.com/redbluegames/unity-mulligan-renamer.git?path=/Assets/RedBlueGames/MulliganRenamer");
#endif
#if !TOOLS_NAUATTR
			Client.Add("com.dbrizov.naughtyattributes");
#endif
#if !TOOLS_ANIM_SEQ
			Client.Add("com.brunomikoski.animationsequencer");
#endif
#if !TOOLS_TWEEN_PLAYABLES
			Client.Add("https://github.com/AnnulusGames/TweenPlayables.git?path=/Assets/TweenPlayables");
#endif
		}
	}
}