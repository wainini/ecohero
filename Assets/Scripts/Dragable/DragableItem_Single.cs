using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableItem_Single : DragableItem
{
    [SerializeField] private DragableItemData data;

    public override DragableItemData[] GetData()
    {
        return new DragableItemData[] { data };
    }
}
