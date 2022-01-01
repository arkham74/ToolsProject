using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Create SelectableData")]
public class SelectableData : ScriptableObject
{
	public ColorBlock colorBlock = ColorBlock.defaultColorBlock;
	public Sprite hover;
	public Sprite normal;
}