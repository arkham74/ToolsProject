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
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using Tag = NaughtyAttributes.TagAttribute;
using Pathfinder.Test;
using Pathfinder;
using UnityEditor;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PathTest : MonoBehaviour
{
	[SerializeField] private Node start;
	[SerializeField] private Node end;

	private void OnDrawGizmos()
	{
		if (start)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(start.transform.position, 1f);
		}
		if (end)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(end.transform.position, 1f);
		}

		if (start && end)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(start.transform.position, end.transform.position);
			DrawPath();
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			end = FindNode();
		}

		if (Input.GetMouseButtonUp(1))
		{
			start = FindNode();
		}
	}

	private void DrawPath()
	{
		if (start && end)
		{
			if (Pathfinder.Pathfinder.TryGetPath(start, end, out List<INode> path))
			{
				IEnumerable<Vector3> enumerable = path.Select(e => e.GetPosition());
				Vector3[] lineSegments = enumerable.ToArray();

				Gizmos.color = Color.yellow;
				for (int i = 0; i < path.Count; i++)
				{
					INode e = path[i];
					Gizmos.DrawSphere(e.GetPosition(), 0.25f);
				}
			}
		}
	}

	private static Node FindNode()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo))
		{
			return hitInfo.collider.GetComponent<Node>();
		}

		return null;
	}
}