using UnityEngine;
using Steamworks;
using System;

namespace JD
{
	public class ShowIfNotSubscribedToAppBase<T> : MonoBehaviour where T : struct, Enum
	{
		[SerializeField] private T appid;

		private void Awake()
		{
			if (!Application.isEditor)
				gameObject.SetActive(!SteamApps.IsSubscribedToApp((uint)(object)appid));
		}
	}
}