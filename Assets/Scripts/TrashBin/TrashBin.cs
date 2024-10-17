using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Material highlightMat;
    [SerializeField] private TrashType binType;
    public GameObject scorePopUpPrefab;

    private Material defaultMat;

    private void Awake()
    {
        defaultMat = sr.material;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out DragableItem dragableItem))
        {
            var datas = dragableItem.GetData();
            foreach(var data in datas)
            {
                ProcessTrash(data);
            }
            Destroy(eventData.pointerDrag);
        }   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out DragableItem dragableItem))
        {
            ToggleHighlight();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(sr.material != defaultMat)
        {
            RemoveHighlight();
        }
    }

    public void ToggleHighlight()
    {
        sr.material = highlightMat;
    }

    public void RemoveHighlight()
    {
        sr.material = defaultMat;
    }

    private void ProcessTrash(DragableItemData data)
    {
        if(data?.Type != binType)
        {
            Debug.Log("Salah bang");
            LevelManager.Instance.CurrentScore -= Convert.ToInt32(50);
            ShowPopUpScore(false);
            return;
        }
        SaveLoadManager.Instance.AddUnlockedItemSaveData(data.Name);
        ShowPopUpScore(true);

        LevelManager.Instance.CurrentScore += Convert.ToInt32(data.Score);
        Debug.Log($"Gokil, kamu dapat {data.Score} dari sampah {data.Name}");
    }

    private void ShowPopUpScore(bool isCorrect)
    {
        GameObject gameObject = Instantiate(scorePopUpPrefab);
        ScorePopUp scorePopUp = gameObject.GetComponent<ScorePopUp>();
        scorePopUp.init(isCorrect);

    }
}
