using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player : MonoBehaviour
{
    [SerializeField] private static int playerInventorySlot = 6;
    public Action OnInventoryUpdate;

    private List<PickableItem> inventory = new();

    public List<PickableItem> Inventory { get { return inventory; } }

    public bool IsInventoryFull => inventory.Count >= playerInventorySlot;

    private void Update()
    {
        MovePlayer();
        SearchInteractables();
        if (inputHandler.InteractInput)
        {
            InteractWithNearest();
            inputHandler.InteractPerformed();
        }
    }


    public void AddItem(PickableItem item)
    {
        if (inventory.Count >= playerInventorySlot)
        {
            Debug.Log("Inventory is full");
            return;
        }

        inventory.Add(item);
        OnInventoryUpdate?.Invoke();
    }

    public void RemoveItem(PickableItem item)
    {
        if(inventory.Contains(item))
        {
            inventory.Remove(item);
            OnInventoryUpdate?.Invoke();
        }
    }
}
