using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler
{
    [SerializeField] private Image iconImage;
    private PickableItem item;
    private Transform dragableParent;

    private DragableItem dragableItem;

    public void InitializeSlot(PickableItem item, Transform dragableParent)
    {
        SetItem(item);
        SetIcon(item.ItemSprite);
        this.dragableParent = dragableParent;
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
        dragableItem = Instantiate(item.Dragable, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, dragableParent);

        dragableItem.transform.position += new Vector3(0, 0, 10);
        eventData.pointerDrag = dragableItem.gameObject;
        RemoveSlotItem();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        eventData.pointerDrag = dragableItem.gameObject;
    }
}
