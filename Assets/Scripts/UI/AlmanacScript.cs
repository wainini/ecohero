using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AlmanacScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private GameObject almanacButtonContainer;

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
        Vector2 buttonPosition = Vector2.zero;
        
        float x_gap = 30;
        float y_gap = 30;

        GameSaveData gameSaveData = SaveLoadManager.Instance.GetGameSaveData;

        Vector2 containerSize = almanacButtonContainer.GetComponent<RectTransform>().rect.size;
        foreach (DragableItemData item in listItemData)
        {
            GameObject gameObject = Instantiate(almanacButtonPrefab, almanacButtonContainer.transform);
            Vector2 buttonSize = gameObject.GetComponent<RectTransform>().rect.size;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            // set pivot kiri atas biar gampang atur posisinya
            rectTransform.pivot = Vector2.up;
            rectTransform.localPosition = buttonPosition;

            float new_x_position = buttonPosition.x + x_gap + buttonSize.x;

            if (new_x_position <= containerSize.x - buttonSize.x)
            {
                buttonPosition.x = new_x_position;
            }
            else
            {
                buttonPosition.x = 0;
                buttonPosition.y -= y_gap + buttonSize.y;
            }            
            bool isEnabled = gameSaveData.ListUnlockedItem.Contains(item.Name);

            AlmanacButton almanacButton = gameObject.GetComponent<AlmanacButton>();
            almanacButton.SetItemData(item, isEnabled);
            almanacButton.onClickEvent.AddListener(almanacDetail.SetUIFromData);
        }
    }
}
