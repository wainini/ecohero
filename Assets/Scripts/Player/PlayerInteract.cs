using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void InteractWithNearest()
    {
        print(NearestInteractable);
        if (NearestInteractable is null) return;
        NearestInteractable.Interact(this.gameObject);
    }

    public void DropItem()
    {
        if(inventory.Count > 0)
        {
            RemoveItem(inventory[0]);
        }
    }
}
