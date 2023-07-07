using UnityEngine;
using Random = UnityEngine.Random;

#if TOOLS_LOCALIZATION
using UnityEngine.Localization.Settings;
#endif

namespace JD
{
	public static class RandomTools
	{
		public static int RandomPosNeg()
		{
			return Random.Range(0, 2) * 2 - 1;
		}

		public static Color RandomColor()
		{
			return new Color(Random.value, Random.value, Random.value);
		}

		public static bool Roll(int sides, out int result)
		{
			result = Random.Range(0, sides);
			return result == 0;
		}
	}
}
