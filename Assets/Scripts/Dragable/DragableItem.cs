using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragableItem : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Material highlightMat;

    [SerializeField] private PickableItem pickable;
    public PickableItem Pickable
    {
        get
        {
            return pickable;
        }
        set
        {
            if (pickable is null) pickable = value;
        }
    }

    private Material defaultMat;

    //Variables used in dragging
    private Vector3 offset;
    private Collider2D coll;
    private Camera mainCam;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        coll = GetComponent<Collider2D>();
        DragableLayerManager.Instance.AddDragable(this);
        defaultMat = sr.material;
    }

    protected virtual void OnDestroy()
    {
        DragableLayerManager.Instance.RemoveDragable(this);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        offset = mainCam.ScreenToWorldPoint(eventData.position) - transform.position;
        DragableLayerManager.Instance.SetOnTop(this);
        ToggleHighlight();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = mainCam.ScreenToWorldPoint(eventData.position) - offset + new Vector3(0, 0, 10);
        ToggleHighlight();
        DisableCollider();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EnableCollider();
    }

    public void EnableCollider() => coll.enabled = true;
    public void DisableCollider() => coll.enabled = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleHighlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveHighlight();
    }

    public void ToggleHighlight()
    {
        sr.material = highlightMat;
    }

    public void RemoveHighlight()
    {
        sr.material = defaultMat;
    }

    public abstract DragableItemData[] GetData();
}
