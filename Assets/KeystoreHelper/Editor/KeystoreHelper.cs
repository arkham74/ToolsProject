using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Startup
{
	private const string PASSWORD = "multitest";

	static Startup()
	{
		Debug.Log("Keystore password set");
		PlayerSettings.Android.keyaliasPass = PASSWORD;
		PlayerSettings.Android.keystorePass = PASSWORD;
	}
}