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
using Object = UnityEngine.Object;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public class DynamicRangeAttributeTest : MonoBehaviour
	{
		[Header("Float Range")]
		[SerializeField] private Vector2 floatMinMax = new Vector2(0, 1);

		[Header("Int Range")]
		[SerializeField] private int intMin = 0;
		[SerializeField][NonReorderable] private Object[] testRange1 = new Object[10];
		[SerializeField][NonReorderable] private List<Object> testRange2 = new List<Object>();

		[Header("Range")]
		[SerializeField][DynamicRange("floatMinMax.x", "floatMinMax.y")] private float floatSlider;
		[SerializeField][DynamicRange("intMin", "testRange1.Array.size")] private int intSlider1;
		[SerializeField][DynamicRange("intMin", "testRange2.Array.size")] private int intSlider2;
	}
}