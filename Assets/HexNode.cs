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
using JD.Pathfind;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HexNode : MonoBehaviour, IAStarNode, IPointerClickHandler
{
	[SerializeField] private HexTest graph;

	public Hex Hex => HexUtils.FromWorld(transform.position / graph.Size).Round();

	public float GetCost(IAStarNode target)
	{
		return 1f;
	}

	public float GetDistance(IAStarNode target)
	{
		HexNode t = target as HexNode;
		return t.transform.Distance(transform);
	}

	public IAStarNode[] GetNeighbours()
	{
		return graph.GetNeighbours(this);
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