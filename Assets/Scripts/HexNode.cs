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
using Random = UnityEngine.Random;
using Text = TMPro.TextMeshProUGUI;
using JD.Pathfind;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HexNode : MonoBehaviour, IAStarNode<HexNode>, IPointerClickHandler
{
	[SerializeField] private HexTest graph;

	public Hex Hex => HexUtils.FromWorld(transform.position / graph.Size).Round();

	public bool Equals(HexNode other)
	{
		return gameObject.Equals(gameObject);
	}

	public bool Equals(HexNode x, HexNode y)
	{
		return x.Equals(y);
	}

	public int GetHashCode(HexNode obj)
	{
		return gameObject.GetHashCode();
	}

	public float GetCost()
	{
		return 1f;
	}

	public float GetDistance(HexNode target)
	{
		HexNode t = target as HexNode;
		return t.transform.Distance(transform);
	}

	public IList<HexNode> GetNeighbours()
	{
		return graph.GetNeighbours(this);
	}

	public IList<HexNode> GetNeighboursNonAlloc(IList<HexNode> list)
	{
		list.Clear();
		foreach (HexNode item in graph.GetNeighbours(this))
		{
			list.Add(item);
		}
		return list;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			graph.SetStart(this);
		}
		else
		{
			graph.SetEnd(this);
		}
	}
}