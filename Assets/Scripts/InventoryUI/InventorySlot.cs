using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetIcon(Sprite iconSprite)
    {
        iconImage.sprite = iconSprite;
    }
}
