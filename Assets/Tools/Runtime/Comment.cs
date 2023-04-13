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
using JD;
using Freya;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tools = JD.Tools;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public class Comment : MonoBehaviour
	{
		[SerializeField][TextArea] private string comment;
	}
}