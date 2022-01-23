using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;

public class FilePrefCallback : BaseCallback
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