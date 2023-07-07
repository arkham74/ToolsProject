using System;

namespace JD
{
	public static class CommandLineTools
	{
		public static bool TryGetArg(string name, out string output)
		{
			output = string.Empty;
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == name && args.Length > i + 1)
				{
					output = args[i + 1];
					return true;
				}
			}
			return false;
		}
	}
}
