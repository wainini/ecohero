using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    [SerializeField] private TrashType binType;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out DragableItem dragableItem))
        {
            ProcessTrash(dragableItem.Data);
            Destroy(eventData.pointerDrag);
        }   
    }

    private void ProcessTrash(DragableItemData data)
    {
        if(data.Type != binType)
        {
            Debug.Log("Salah bang");
            return;
        }

        Debug.Log($"Gokil, kamu dapat {data.Score} dari sampah {data.Name}");
    }
}
