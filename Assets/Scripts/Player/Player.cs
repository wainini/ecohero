using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    private List<Item> inventory = new();

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
        inventory.Add(item);
        //update UI using event Action

        Debug.Log($"Added item {item.name}");
    }

    public void RemoveItem(Item item)
    {
        if(inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"Removed item {item.name}");
        }
        //update UI using event Action
    }
}
