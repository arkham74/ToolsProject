using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JD
{
	public class VersionNumberText : MonoBehaviour
	{
		[SerializeField] private Text text;

		private void Reset()
		{
			text = GetComponent<Text>();
		}

		private void Start()
		{
			text.text = Application.version;
		}
	}
}