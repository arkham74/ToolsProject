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
using JD.Pathfind;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class AStarTest : MonoBehaviour
{
	private readonly TestCell[,] grid = new TestCell[10, 10];

	private void Start()
	{
		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				grid[x, y] = new TestCell(grid, new Vector3Int(x, 0, y));
			}
		}
	}

	private void OnDrawGizmos()
	{
		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				Gizmos.DrawWireCube(new Vector3(x, 0, y), Vector3.one.SetY(0));
			}
		}

		if (!Application.isPlaying) return;

		Vector3 screenPos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		if (plane.Raycast(ray, out RaycastHit hit))
		{
			TestCell goal = new TestCell(grid, hit.point.RoundToInt());
			TestCell start = new TestCell(grid, Vector3Int.zero);
			var path = AStar<TestCell>.GetPath(start, goal);
			foreach (var item in path)
			{
				Gizmos.DrawWireCube(item.Position, Vector3.one.SetY(0));
			}
		}
	}
}

public class TestCell : IAStarNode<TestCell>
{
	private readonly TestCell[,] grid;
	private readonly Vector2Int position;
	public Vector3 Position => new Vector3(position.x, 0, position.y);

	public TestCell(TestCell[,] grid, Vector3Int position)
	{
		this.grid = grid;
		this.position = new Vector2Int(position.x, position.z);
	}

	public float GetCost()
	{
		return 1;
	}

	public float GetDistance(TestCell target)
	{
		return Vector2.Distance(target.position, position);
	}

	private bool Create(int x, int y)
	{
		if (x < 0) return false;
		if (y < 0) return false;
		if (x >= 10) return false;
		if (y >= 10) return false;
		return true;
	}

	public bool Equals(TestCell other)
	{
		return position.Equals(other.position);
	}

	public bool Equals(TestCell x, TestCell y)
	{
		return x.Equals(y);
	}

	public int GetHashCode(TestCell obj)
	{
		return position.GetHashCode();
	}

	public IList<TestCell> GetNeighbours()
	{
		return GetNeighboursNonAlloc(new List<TestCell>());
	}

	public IList<TestCell> GetNeighboursNonAlloc(IList<TestCell> list)
	{
		list.Clear();

		if (Create(position.x + 1, position.y + 1))
			list.Add(grid[position.x + 1, position.y + 1]);

		if (Create(position.x + 1, position.y - 1))
			list.Add(grid[position.x + 1, position.y - 1]);

		if (Create(position.x - 1, position.y + 1))
			list.Add(grid[position.x - 1, position.y + 1]);

		if (Create(position.x - 1, position.y - 1))
			list.Add(grid[position.x - 1, position.y - 1]);

		return list;
	}
}