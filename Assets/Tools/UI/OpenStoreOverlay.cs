using UnityEngine;
using JD;
using UnityEngine.UI;
using Steamworks;

namespace JD
{
	public class OpenStoreOverlay : MonoBehaviour
	{
		[SerializeField] private SteamAppIds appid;
		[SerializeField] private Button button;

		private void Awake()
		{
			button.ReplaceListener(Button_Open);
		}

		private void Button_Open()
		{
			SteamFriends.OpenStoreOverlay((uint)appid);
		}
	}
}