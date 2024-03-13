using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragableItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private DragableItemData data;
    public DragableItemData Data { get { return data; } }

    private PickableItem pickable;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("AAAAA");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position= Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position += new Vector3(0, 0, 10);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
