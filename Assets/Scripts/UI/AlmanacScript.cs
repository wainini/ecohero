using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private ScrollRect almanacButtonScrollRect;

    [SerializeField]
    private GameObject almanacButtonPrefab;

    [SerializeField]
    private AlmanacDetail almanacDetail;

    private Canvas canvas;

    private Dictionary<string, AlmanacButton> itemNameToButtonComponent = new();
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        SpawnAlmanacButton();
    }


    private void UnlockAlmanacItem(string itemName) 
    {
        var almanacButtonComponent = itemNameToButtonComponent[itemName];
        almanacButtonComponent.SetEnabled(true);
    }

    private void SpawnAlmanacButton()
    {
        DragableItemData[] listItemData = Resources.LoadAll<DragableItemData>("itemData");        
        GameSaveData gameSaveData = SaveLoadManager.Instance.GetGameSaveData;
        SaveLoadManager.Instance.OnItemUnlocked.AddListener(UnlockAlmanacItem);

        foreach (DragableItemData item in listItemData)
        {
            GameObject gameObject = Instantiate(almanacButtonPrefab, almanacButtonScrollRect.content);
       
            bool isEnabled = gameSaveData.ListUnlockedItem.Contains(item.Name);

            AlmanacButton almanacButton = gameObject.GetComponent<AlmanacButton>();
            almanacButton.SetItemData(item, isEnabled);
            almanacButton.onClickEvent.AddListener(almanacDetail.SetUIFromData);

            itemNameToButtonComponent.Add(item.Name, almanacButton);
        }
    }
}
