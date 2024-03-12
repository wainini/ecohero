using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractables
{

    [SerializeField] private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; }}

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
