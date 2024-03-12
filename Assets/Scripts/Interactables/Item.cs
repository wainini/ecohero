using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractables
{


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public virtual void Interact(GameObject actor)
    {
        if(actor.TryGetComponent(out Player player))
        {
            player.AddItem(this);
        }
    }
}
