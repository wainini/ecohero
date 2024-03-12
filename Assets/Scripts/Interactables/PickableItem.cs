using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IInteractables
{
    [SerializeField] private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; }}

    private Dragable dragable;

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
