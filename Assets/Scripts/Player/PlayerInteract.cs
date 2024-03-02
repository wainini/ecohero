using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void InteractWithNearest()
    {
        if (nearestInteractable is null) return;
        nearestInteractable.Interact(this.gameObject);
    }
}
