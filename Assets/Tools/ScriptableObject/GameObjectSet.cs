using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;
using Text = TMPro.TextMeshProUGUI;
using Random = UnityEngine.Random;

using Object = UnityEngine.Object;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	[CreateAssetMenu(fileName = "GameObjectSet", menuName = "ScriptableObject/RuntimeSet/GameObject", order = 0)]
	public class GameObjectSet : RuntimeSet<GameObject>
	{

	}
}