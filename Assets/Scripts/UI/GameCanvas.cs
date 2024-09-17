using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject raycastOverlay;


    [SerializeField]
    private GameObject almanacToggleButton;

    [SerializeField]
    private TextMeshProUGUI labelScore;
    [SerializeField]
    private TextMeshProUGUI labelTime;
    [SerializeField]
    private GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameModeChanged.AddListener(SetActiveSeperateTrashButton);
        LevelManager.Instance.ScoreChanged.AddListener(SetScoreUI);
    }

    private void SetScoreUI(int Score)
    {
        labelScore.text = Score.ToString();
    }

    private void Update()
    {
        labelTime.text = Convert.ToInt32(LevelManager.Instance.currentTime).ToString();
    }
    public void OpenAlmanac()
    {
        MenuManager.Instance.OpenMenuOverlap("AlmanacMenu");
    }

    public void OpenPauseMenu()
    {
        MenuManager.Instance.OpenMenuOverlap("PauseMenu");
    }

    public void BackToCollectTrash()
    {
        GameManager.Instance.EnterCollectTrashMode();
    }

    public void SetActiveSeperateTrashButton(GameMode gameMode)
    {
        if (gameMode == GameMode.SeperateTrash)
        {
            almanacToggleButton.SetActive(true);
            backButton.SetActive(true);
            raycastOverlay.SetActive(false);    
        }
        else
        {
            almanacToggleButton.SetActive(false);
            backButton.SetActive(false);
            raycastOverlay.SetActive(true);    
        }
    }
}
