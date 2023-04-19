using UnityEngine;
#if TOOLS_NAUATTR
using NaughtyAttributes;
#endif

namespace JD
{
	public class IgnoreLayerCollision : MonoBehaviour
	{
		[SerializeField] private bool ignore = true;
#if TOOLS_NAUATTR
		[SerializeField][Layer] private int layer1;
		[SerializeField][Layer] private int layer2;
#else
		[SerializeField] private int layer1;
		[SerializeField] private int layer2;
#endif

		private void Awake()
		{
			Physics.IgnoreLayerCollision(layer1, layer2, ignore);
		}
	}
}