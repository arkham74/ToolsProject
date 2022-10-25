using System.Text;
using UnityEngine;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class Pools
	{
		private static readonly StringBuilder sb = new StringBuilder();
		private static readonly StringBuilder sbc = new StringBuilder();
		private static readonly MaterialPropertyBlock mpb = new MaterialPropertyBlock();

		public static StringBuilder StringBuilder
		{
			get
			{
				sb.Clear();
				return sb;
			}
		}

		public static StringBuilder StringBuilderCopy
		{
			get
			{
				sbc.Clear();
				return sbc;
			}
		}

		public static MaterialPropertyBlock MaterialPropertyBlock
		{
			get
			{
				mpb.Clear();
				return mpb;
			}
		}
	}
}
