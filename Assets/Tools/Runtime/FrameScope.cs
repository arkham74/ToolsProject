using System;
using UnityEngine.Rendering;

namespace JD
{
	public struct FrameScope : IDisposable
	{
		private ScriptableRenderContext context;
		private CommandBuffer cmd;
		private string name;

		public FrameScope(ScriptableRenderContext context, CommandBuffer cmd, string name)
		{
			this.context = context;
			this.cmd = cmd;
			this.name = name;
			Begin();
		}

		public void Begin()
		{
			cmd.Clear();
			cmd.BeginSample(name);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		public void End()
		{
			cmd.Clear();
			cmd.EndSample(name);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		public void Dispose()
		{
			End();
		}
	}
}