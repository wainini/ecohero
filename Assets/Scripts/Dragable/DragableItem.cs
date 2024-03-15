using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;


public class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
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

    private Vector3 offset;
    private Collider2D coll;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        DragableLayerManager.Instance.AddDragable(this);
    }

    private void OnDisable()
    {
        DragableLayerManager.Instance.RemoveDragable(this);
    }
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        offset = mainCam.ScreenToWorldPoint(eventData.position) - transform.position;
        DragableLayerManager.Instance.SetOnTop(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = mainCam.ScreenToWorldPoint(eventData.position) - offset + new Vector3(0, 0, 10);
        coll.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        coll.enabled = true;
    }
}
