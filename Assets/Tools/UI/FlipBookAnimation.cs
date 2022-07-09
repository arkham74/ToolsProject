using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
#if TOOLS_NAUATTR
using NaughtyAttributes;
using Tag = NaughtyAttributes.TagAttribute;
#endif

namespace CustomTools
{
	public class FlipBookAnimation : MonoBehaviour
	{
		public Image target;
		public float frameRate = 60;
		public bool loop = true;
		public List<Sprite> frames = new List<Sprite>();

#if UNITY_EDITOR
		public string path;
#if TOOLS_NAUATTR
		[Button]
#endif
		private void Load()
		{
			string[] obj = AssetDatabase.FindAssets("t:sprite", new[] { path });
			obj.LogWarning();
			frames = obj.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<Sprite>).ToList();
		}
#endif

		protected void Reset()
		{
			target = GetComponent<Image>();
		}

		protected void OnEnable()
		{
			StartCoroutine(Animate());
		}

		private IEnumerator Animate()
		{
			IEnumerator e = frames.GetEnumerator();
			WaitForSecondsRealtime seconds = new WaitForSecondsRealtime(1f / frameRate);
			while (true)
			{
				if (e.MoveNext())
				{
					target.sprite = e.Current as Sprite;
					yield return seconds;
				}
				else if (loop)
				{
					e.Reset();
				}
				else
				{
					break;
				}
			}
		}
	}
}