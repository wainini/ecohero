using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform dragablesParent;
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
        List<PickableItem> playerInventory = player.Inventory;
        int idx = 0;
        foreach(PickableItem item in playerInventory)
        {
            slots[idx].InitializeSlot(item, dragablesParent);
            idx++;
        }
        for(int i = idx; i < slots.Count; i++)
        {
            slots[idx].RemoveSlotItem();
        }
    }
}
