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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[CreateAssetMenu(fileName = "TransformSet", menuName = "ScriptableObject/RuntimeSet/Transform", order = 0)]
public class TransformSet : RuntimeSet<Transform>
{

}