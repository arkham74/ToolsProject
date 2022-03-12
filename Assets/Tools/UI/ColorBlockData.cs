using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/ColorBlockData")]
public class ColorBlockData : ScriptableObject
{
	public ColorBlock colorBlock = ColorBlock.defaultColorBlock;
}