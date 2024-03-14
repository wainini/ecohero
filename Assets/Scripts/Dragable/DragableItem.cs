using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private DragableItemData data;
    public DragableItemData Data { get { return data; } }

    private PickableItem pickable;
    public PickableItem Pickable
    {
        get
        {
            return pickable;
        }
        set
        {
            if(pickable is null) pickable = value;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position= Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position += new Vector3(0, 0, 10);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
