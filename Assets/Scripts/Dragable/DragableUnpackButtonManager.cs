using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUnpackButtonManager : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private static DragableUnpackButtonManager instance;
    public static DragableUnpackButtonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DragableUnpackButtonManager>();
            }
            return instance;
        }
    }
    private List<DragablePack> dragablePacksInGame = new();
    private DragablePack dragablePackInFocus;

    private void OnDisable()
    {
        instance = null;
    }


    public void AddDragablePack(DragablePack pack)
    {
        dragablePacksInGame.Add(pack);
    }

    public void RemoveDragablePack(DragablePack pack)
    {
        dragablePacksInGame.Remove(pack);
    }

    public void SetFocus(DragablePack pack)
    {
        dragablePackInFocus = pack;
        
    }

    public void DisableAllDragableButton()
    {
        foreach(DragablePack dragablePack in dragablePacksInGame)
        {
            dragablePack.DisableUnpackButton();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DisableAllDragableButton();
    }

    public void OnDrag(PointerEventData eventData)
    {
        DisableAllDragableButton();
    }
}
