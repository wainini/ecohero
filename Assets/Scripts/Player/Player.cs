using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] private int playerInventorySlot = 6;
    public Action OnInventoryUpdate;

    private List<Item> inventory = new();

    public List<Item> Inventory { get { return inventory; } }

    private void Update()
    {
        MovePlayer();
        SearchInteractables();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInteractInput += InteractWithNearest;
        InputManager.Instance.OnDropInput += DropItem;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteractInput -= InteractWithNearest;
        InputManager.Instance.OnDropInput -= DropItem;
    }

    public void AddItem(Item item)
    {
        if (inventory.Count >= playerInventorySlot)
        {
            Debug.Log("Inventory is full");
            return;
        }

        inventory.Add(item);
        OnInventoryUpdate?.Invoke();
        Debug.Log($"Added item {item.name}");
    }

    public void RemoveItem(Item item)
    {
        if(inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"Removed item {item.name}");
            OnInventoryUpdate?.Invoke();
        }
    }
}
