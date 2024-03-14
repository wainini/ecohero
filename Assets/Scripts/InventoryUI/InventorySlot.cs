using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Image iconImage;

    private InventoryUI inventoryUI;

    private PickableItem item;
    private Transform dragableParent;

    private DragableItem dragableItem;

    public void InitializeSlot( Transform dragableParent, InventoryUI inventoryUI)
    {
        this.dragableParent = dragableParent;
        this.inventoryUI = inventoryUI;     
    }

    public void UpdateSlot(PickableItem item)
    {
        SetItem(item);
        SetIcon(item.ItemSprite);
    }

    public void RemoveSlotItem()
    {
        SetItem(null);
        SetIcon(null);
    }

    public void SetItem(PickableItem item)
    {
        this.item = item;   
    }

    public void SetIcon(Sprite iconSprite)
    {
        iconImage.sprite = iconSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(item is null) return;

        //instantiate the dragable version of the item
        dragableItem = Instantiate(item.Dragable, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, dragableParent);
        dragableItem.Pickable = item;
        //so it's in front of camera
        dragableItem.transform.position += new Vector3(0, 0, 10);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(item is null) return;
        //enable to instantly drag the newly instantiated item
        eventData.pointerDrag = dragableItem.gameObject;
        dragableItem = null;
        inventoryUI.PlayerReference.RemoveItem(item);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Droped");
        if(eventData.pointerDrag.TryGetComponent(out DragableItem dragableItem))
        {
            inventoryUI.PlayerReference.AddItem(dragableItem.Pickable);
            Destroy(eventData.pointerDrag);
        }
    }
}
