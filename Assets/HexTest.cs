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
using SD;
using JD.Pathfind;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HexTest : MonoBehaviour
{
	[SerializeField] private HexDirection direction;
	[SerializeField] private SerializableDictionary<Hex, HexNode> cells;

	public float Size => 0.5f;

	private HexNode start;
	private HexNode end;

	private void OnDrawGizmos()
	{
		// GizmosTools.SetColor(Color.red);

		// foreach (Transform child in transform)
		// {
		// 	Vector3 world = child.position;

		// 	Hex hex = HexUtils.FromWorld(world / Size);
		// 	hex = hex.Round();
		// 	world = hex.ToWorld() * Size;

		// 	child.position = world;
		// 	GizmosTools.DrawHex(world, Size);
		// }


		GizmosTools.SetColor(Color.blue);

		if (start && end)
		{
			GizmosTools.DrawHex(start.Hex.ToWorld() * Size, Size);
			GizmosTools.DrawHex(end.Hex.ToWorld() * Size, Size);

			if (direction != 0 && Mathf.IsPowerOfTwo((int)direction))
			{
				GizmosTools.SetColor(Color.red);
				GizmosTools.DrawHex(start.Hex.GetNeighbour(direction).ToWorld() * Size, Size);
				GizmosTools.DrawHex(end.Hex.GetNeighbour(direction).ToWorld() * Size, Size);
			}

			// var list = AStar.GetPath(start, end);
			// foreach (HexNode node in list)
			// {
			// 	GizmosTools.DrawHex(node.Hex.ToWorld() * Size, Size);
			// }
		}
	}

	private void Reset()
	{
		cells.Clear();

		foreach (HexNode item in GetComponentsInChildren<HexNode>())
		{
			cells[item.Hex] = item;
		}
	}

	public HexNode[] GetNeighbours(HexNode current)
	{
		List<HexNode> list = new List<HexNode>();

		foreach (var item in current.Hex.GetNeighbours())
		{
			if (cells.TryGetValue(item, out HexNode node))
			{
				list.Add(node);
			}
		};

		return list.ToArray();
	}

	public void SetStart(HexNode hexNode)
	{
		start = hexNode;
	}

	public void SetEnd(HexNode hexNode)
	{
		end = hexNode;
	}
}