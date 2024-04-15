using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlmanacButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Image itemImage;

    private Button button;
    private DragableItemData itemData;

    private bool isEnabled;
    public UnityEvent<DragableItemData> onClickEvent;
    void OnClickEvent()
    {
        onClickEvent.Invoke(itemData);
    }

    public void SetItemData(DragableItemData itemData, bool isEnabled)
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickEvent);

        this.itemData = itemData;
        itemImage.sprite = itemData.Sprite;
        SetEnabled(isEnabled);
    }

    public void SetEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
        if (isEnabled)
        {
            itemImage.color = Color.gray;
            button.enabled = false;
        }
        else
        {
            itemImage.color = Color.white;
            button.enabled = true;
        }
    }
}
