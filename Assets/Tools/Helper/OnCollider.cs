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

namespace CustomTools
{
	public class OnCollider : MonoBehaviour
	{
		[field: SerializeField] public UnityEvent<Collider2D> OnColliderEvent { get; private set; }

		private void OnTriggerEnter2D(Collider2D other)
		{
			OnColliderEvent.Invoke(other);
		}
	}
}