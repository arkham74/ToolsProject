using System;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace JD.Editor
{
	[InitializeOnLoad]
	public class ImportPackages
	{
		static ImportPackages()
		{
#if !TOOLS_TOOLBAR
			ClientAdd("https://github.com/marijnz/unity-toolbar-extender.git");
#endif
#if !TOOLS_FREYA
			ClientAdd("https://github.com/FreyaHolmer/Mathfs.git");
#endif
#if !TOOLS_COMP_VIS
			ClientAdd("com.needle.compilation-visualizer");
#endif
#if !TOOLS_RENAMER
			ClientAdd("https://github.com/redbluegames/unity-mulligan-renamer.git?path=/Assets/RedBlueGames/MulliganRenamer");
#endif
#if !TOOLS_NAUATTR
			ClientAdd("com.dbrizov.naughtyattributes");
#endif
#if !TOOLS_ANIM_SEQ
			ClientAdd("com.brunomikoski.animationsequencer");
#endif
#if !TOOLS_TWEEN_PLAYABLES
			ClientAdd("https://github.com/AnnulusGames/TweenPlayables.git?path=/Assets/TweenPlayables");
#endif
#if !TOOLS_SG_VARS
			ClientAdd("https://github.com/Cyanilux/ShaderGraphVariables.git");
#endif
		}

		private static void ClientAdd(string link)
		{
			Debug.LogWarning(link);
			Client.Add(link);
		}
	}
}