using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots;

    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        if(player is null)
        {
            player = FindObjectOfType<Player>();
        }
        player.OnInventoryUpdate += UpdateInventory;
    }

    private void OnDisable()
    {
        player.OnInventoryUpdate -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        List<Item> playerInventory = player.Inventory;
        int idx = 0;
        foreach(Item item in playerInventory)
        {
            slots[idx].SetIcon(item.ItemSprite);
            idx++;
        }
        for(int i = idx; i < slots.Count; i++)
        {
            slots[idx].SetIcon(null);
        }
    }
}
