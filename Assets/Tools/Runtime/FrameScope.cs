using System;
using UnityEngine.Rendering;

namespace JD
{
	public struct FrameScope : IDisposable
	{
		private CommandBuffer cmd;
		private string name;

		public FrameScope(CommandBuffer cmd, string name)
		{
			this.cmd = cmd;
			this.name = name;
			Begin();
		}

		public void Begin()
		{
			cmd.BeginSample(name);
		}

		public void End()
		{
			cmd.EndSample(name);
		}

		public void Dispose()
		{
			End();
		}
	}
}