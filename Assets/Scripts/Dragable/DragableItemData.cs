using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class DragableItemData : ScriptableObject
{
    public string Name;
    public TrashType Type;
    public float Score;
    public string Description;
    public Sprite Sprite;
}

public enum TrashType
{
    Plastic,
    Metal,
    Paper,
    B3
}
