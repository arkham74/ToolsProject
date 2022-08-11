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

#if TOOLS_NAUATTR
using NaughtyAttributes;
using Tag = NaughtyAttributes.TagAttribute;
#endif

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public class CurrentYear : MonoBehaviour
	{
		public int Year => DateTime.Now.Year;
		public int Month => DateTime.Now.Month;
		public int Day => DateTime.Now.Day;
		public int Hour => DateTime.Now.Hour;
		public int Minute => DateTime.Now.Minute;
		public int Second => DateTime.Now.Second;
		public int Millisecond => DateTime.Now.Millisecond;
	}
}