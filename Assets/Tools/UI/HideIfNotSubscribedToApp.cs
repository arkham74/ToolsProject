using UnityEngine;
using JD;
using Steamworks;

namespace JD
{
	public class HideIfNotSubscribedToApp : MonoBehaviour
	{
		[SerializeField] private SteamAppIds appid;

		private void Awake()
		{
			if (!Application.isEditor)
				gameObject.SetActive(!SteamApps.IsSubscribedToApp((uint)appid));
		}
	}
}