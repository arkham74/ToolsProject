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
using NaughtyAttributes;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JD
{
	public abstract class ColliderCallback : BaseCallback
	{
		[SerializeField] protected UnityEvent<Collider> OnEnter;
		[SerializeField] protected UnityEvent<Collider> OnExit;
		[SerializeField] protected UnityEvent<Collider> OnStay;

		protected bool CheckTagAndLayer(Collider other)
		{
			if (objectTag == string.Empty || other.CompareTag(objectTag))
			{
				int mask = 1 << other.gameObject.layer;
				if ((objectMask & mask) != 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}