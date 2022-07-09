using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CustomTools
{
	public class FileBasedPrefsCallback : BaseCallback
	{
		public string key = "PREF_KEY";
		public bool defaultValue;
		public UnityEvent<bool> onEvent;

		public void SetPref(bool value)
		{
			FileBasedPrefs.SetBool(key, value);
		}

		protected override void Trigger()
		{
			onEvent.Invoke(FileBasedPrefs.GetBool(key, defaultValue));
		}
	}
}