using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragablePack : DragableItem, IPointerClickHandler
{
    [SerializeField] private List<DragableItem> dragablesInside = new();
    [SerializeField] private Canvas unpackButtonCanvas;

    protected override void Awake()
    {
        base.Awake();

        DragableUnpackButtonManager.Instance.AddDragablePack(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        DragableUnpackButtonManager.Instance.RemoveDragablePack(this);
    }

    protected virtual void HandleOnClick()
    {
        DragableUnpackButtonManager.Instance.DisableAllDragableButton();
        unpackButtonCanvas.enabled = true;
    }

    public void DisableUnpackButton()
    {
        unpackButtonCanvas.enabled = false;
    }


    [ContextMenu("Unpack")]
    public virtual void OpenPack()
    {
        //make it so this DragablePack is on top of everything
        DragableLayerManager.Instance.SetOnTop(this);

        foreach(DragableItem_Single dragable in dragablesInside)
        {
            dragable.gameObject.SetActive(true);
            dragable.EnableCollider();
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
        Destroy(unpackButtonCanvas.gameObject);
        Destroy(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        HandleOnClick();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        DisableUnpackButton();
    }

    public override DragableItemData[] GetData()
    {
        DragableItemData[] datas = new DragableItemData[dragablesInside.Count];

        for(int i = 0; i < dragablesInside.Count; i++)
        {
            datas[i] = dragablesInside[i].GetData()[0];
        }
        return datas;
    }
}
