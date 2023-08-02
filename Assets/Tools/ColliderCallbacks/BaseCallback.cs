using UnityEngine;
using TagAttribute = NaughtyAttributes.TagAttribute;

namespace JD
{
	public abstract class BaseCallback : MonoBehaviour
	{
		[SerializeField][Tag] protected string objectTag = "";
		[SerializeField] protected LayerMask objectMask = -1;
	}
}