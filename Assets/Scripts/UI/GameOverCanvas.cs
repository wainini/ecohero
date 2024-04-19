using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        LevelManager.Instance.GameOverEvent.AddListener(ShowGameOverScreen);
    }

    private void ShowGameOverScreen()
    {
        scoreText.text = "Score : " + LevelManager.Instance.CurrentScore;
        canvas.enabled = true;
    }
}
