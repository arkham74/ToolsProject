using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using System;

namespace JD
{
	public class OpenStoreOverlayBase<T> : MonoBehaviour where T : struct, Enum
	{
		[SerializeField] private T appid;
		[SerializeField] private Button button;

		private void Awake()
		{
			button.ReplaceListener(Button_Open);
		}

		private void Button_Open()
		{
			SteamFriends.OpenStoreOverlay((uint)(object)appid);
		}
	}
}