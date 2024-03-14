using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform dragablesParent;
    [SerializeField] private List<InventorySlot> slots;

    public Player PlayerReference { get; private set; }
    private void Start()
    {
        PlayerReference = FindObjectOfType<Player>();
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.InitializeSlot(dragablesParent, this);
        }
    }


    private void OnEnable()
    {
        if(PlayerReference is null)
        {
            PlayerReference = FindObjectOfType<Player>();
        }
        PlayerReference.OnInventoryUpdate += UpdateInventory;
    }

    private void OnDisable()
    {
        PlayerReference.OnInventoryUpdate -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        List<PickableItem> playerInventory = PlayerReference.Inventory;
        int idx = 0;
        foreach(PickableItem item in playerInventory)
        {
            Debug.Log(item.name);
            slots[idx].UpdateSlot(item);
            idx++;
        }
        for(int i = idx; i < slots.Count; i++)
        {
            slots[idx].RemoveSlotItem();
        }
    }
}
