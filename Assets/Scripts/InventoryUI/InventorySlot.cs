using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler, IInitializePotentialDragHandler, IPointerUpHandler
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

    //pointerdown and drag is here so OnInitializePotentialDrag and OnPointerUp works (why unity, why)
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
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

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (item is null) return;

        //instantiate the dragable version of the item
        dragableItem = Instantiate(item.Dragable, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, dragableParent);
        dragableItem.Pickable = item;
        dragableItem.ToggleHighlight();
        //so it's in front of camera
        dragableItem.transform.position += new Vector3(0, 0, 10);

        SetIcon(null); //so that EventSystem doesn't think the pointer is dragging the Icon UI
        eventData.pointerDrag = dragableItem.gameObject;
        inventoryUI.PlayerReference.RemoveItem(item);
        dragableItem = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //This will call the drop event if the mouse didn't move at all (drag not trigger as well as OnDrop). 
        //This will still go through if you click an empty slot, although it won't do anything because the eventData wont have DragableItem script
        if (eventData.dragging ) return;
        OnDrop(eventData);
    }
}
