using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragableLayerManager : MonoBehaviour
{
    public static DragableLayerManager Instance;

    [SerializeField] private string dragableSortingLayer;
    private int currentSortingOrder = -30000;

    private List<DragableItem> allDragablesInGame = new();
    private Dictionary<DragableItem, SpriteRenderer> dragableSpriteRenderers = new();

    private void Awake()
    {
        if(Instance is not null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void AddDragable(DragableItem item)
    {
        allDragablesInGame.Add(item);

        SpriteRenderer dragableSR = item.GetComponent<SpriteRenderer>();
        dragableSR.sortingLayerID = SortingLayer.NameToID(dragableSortingLayer);

        dragableSpriteRenderers.Add(item, dragableSR);
        SetOnTop(item);
    }

    public void RemoveDragable(DragableItem item)
    {
        allDragablesInGame.Remove(item);
        dragableSpriteRenderers.Remove(item);

        //ResetAllSortingOrder();
    }

    //Implement this later
    public void OnModeChange()
    {
        ResetAllSortingOrder();
    }

    public void SetOnTop(DragableItem item)
    {
        dragableSpriteRenderers[item].sortingOrder = currentSortingOrder;

        if(currentSortingOrder >= 30000) //highest sorting order is 32767
        {
            ResetAllSortingOrder();
            return;
        }

        currentSortingOrder++;
    }

    private void ResetAllSortingOrder()
    {
        currentSortingOrder = -30000; //lowest sorting order is -32768

        if (dragableSpriteRenderers.Count == 0) return;
        
        var sortedDictionary = dragableSpriteRenderers.OrderBy(x => x.Value);
        foreach(KeyValuePair<DragableItem, SpriteRenderer> dragable in sortedDictionary)
        {
            dragable.Value.sortingOrder = currentSortingOrder;
            currentSortingOrder++;
        }
    }
}
