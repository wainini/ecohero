using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void InteractWithNearest()
    {
        print(nearestInteractable);
        if (nearestInteractable as Object == null) return;
        nearestInteractable.Interact(this.gameObject);
        if (nearestInteractable as Object == null) nearestInteractable = null;
    }

    public void DropItem()
    {
        if(inventory.Count > 0)
        {
            RemoveItem(inventory[0]);
        }
    }
}
