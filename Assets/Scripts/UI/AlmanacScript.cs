using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        SpawnAlmanacButton();
    }

    private void SpawnAlmanacButton()
    {
        DragableItemData[] listItemData = Resources.LoadAll<DragableItemData>("itemData");        
        GameSaveData gameSaveData = SaveLoadManager.Instance.GetGameSaveData;

        foreach (DragableItemData item in listItemData)
        {
            GameObject gameObject = Instantiate(almanacButtonPrefab, almanacButtonScrollRect.content);
       
            bool isEnabled = gameSaveData.ListUnlockedItem.Contains(item.Name);

            AlmanacButton almanacButton = gameObject.GetComponent<AlmanacButton>();
            almanacButton.SetItemData(item, isEnabled);
            almanacButton.onClickEvent.AddListener(almanacDetail.SetUIFromData);
        }
    }
}
