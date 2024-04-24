using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableItem : MonoBehaviour, IInteractables
{
    public Action<PickableItem> OnPickUp;

    [SerializeField] private Sprite itemSprite;

    public Sprite ItemSprite { get { return itemSprite; }}

    [SerializeField] private DragableItem dragable;

    public DragableItem Dragable { get {  return dragable; }}

    private void Awake()
    {
        
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public virtual void Interact(GameObject actor)
    {
        if(actor.TryGetComponent(out Player player))
        {
            OnPickUp?.Invoke(this);
            player.AddItem(this);
            Destroy(this.gameObject);
        }
    }
}
