using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragablePack : DragableItem
{
    [SerializeField] private List<DragableItem> dragablesInside = new();

    [ContextMenu("Unpack")]
    protected virtual void OpenPack()
    {
        //make it so this DragablePack is on top of everything
        DragableLayerManager.Instance.SetOnTop(this);

        foreach(DragableItem dragable in dragablesInside)
        {
            dragable.gameObject.SetActive(true);
            //organize every DragableItems inside the pack
            DragableLayerManager.Instance.SetOnTop(dragable);
        }

        PackOpened();
    }

    /// <summary>
    /// Default implementation is to delete the pack gameobject. Please override if you want the pack to spawn something
    /// </summary>
    protected virtual void PackOpened()
    {
        transform.DetachChildren();
        Destroy(this.gameObject);
    }
}
