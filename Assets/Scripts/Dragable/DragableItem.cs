using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;


public class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
    [SerializeField] private DragableItemData data;
    public DragableItemData Data { get { return data; } }

    [SerializeField] private PickableItem pickable;
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

    //Variables used in dragging
    private Vector3 offset;
    private Collider2D coll;
    private Camera mainCam;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        coll = GetComponent<Collider2D>();
        DragableLayerManager.Instance.AddDragable(this);
    }

    protected virtual void OnDestroy()
    {
        DragableLayerManager.Instance.RemoveDragable(this);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        offset = mainCam.ScreenToWorldPoint(eventData.position) - transform.position;
        DragableLayerManager.Instance.SetOnTop(this);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = mainCam.ScreenToWorldPoint(eventData.position) - offset + new Vector3(0, 0, 0);
        DisableCollider();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EnableCollider();
    }

    public void EnableCollider() => coll.enabled = true;
    public void DisableCollider() => coll.enabled = false;
}
