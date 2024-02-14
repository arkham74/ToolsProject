using System.Threading.Tasks;
using UnityEngine;
using Redcode.Awaiting;

namespace JD
{
	public static class AsyncExtensions
	{
		public static async Task WaitAsync(this AsyncOperation handle)
		{
			while (!handle.isDone)
			{
				await Yield.WaitForEndOfFrame();
			}
		}
	}
}