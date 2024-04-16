using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacDetail : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI description;
    public void SetUIFromData(DragableItemData itemData)
    {
        itemName.text = itemData.Name;
        image.sprite = itemData.Sprite;
        description.text = itemData.Description;
        image.color = Color.white;
    }
}
