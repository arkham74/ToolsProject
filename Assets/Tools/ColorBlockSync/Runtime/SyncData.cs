using UnityEngine;
using UnityEngine.UI;

namespace JD.DataSync
{
	public abstract class SyncData : ScriptableObject
	{
		public abstract void Normal(Graphic target);
		public abstract void Highlight(Graphic target);
		public abstract void Disabled(Graphic target);
	}
}