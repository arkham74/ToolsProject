using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace JD.Shared
{
	[CreateAssetMenu(menuName = "ScriptableObject/SharedString")]
	public class SharedString : Shared<string>
	{
	}
}