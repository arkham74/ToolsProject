using UnityEngine;
using JD;
using Steamworks;

namespace JD
{
	public class ShowIfNotSubscribedToApp : MonoBehaviour
	{
		[SerializeField] private bool invert;
		[SerializeField] private SteamAppIds appid;

		private void Awake()
		{
			if (!Application.isEditor)
			{
				uint id = (uint)appid;
				bool isSubscribed = SteamApps.IsSubscribedToApp(id);
				bool isOn = invert ? isSubscribed : !isSubscribed;
				gameObject.SetActive(isOn);
			}
		}
	}
}