using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField]
    private TextMeshProUGUI playerScoreText;

    [SerializeField]
    private TextMeshProUGUI targetScoreText;

    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        LevelManager.Instance.GameOverEvent.AddListener(ShowGameOverScreen);
    }

    private void ShowGameOverScreen()
    {
        AudioManager.Instance.StopSound("GameMusic");
        GameManager.Instance.IsGamePaused = true;
        playerScoreText.text = "Your Score : " + LevelManager.Instance.CurrentScore;
        targetScoreText.text = "Target Score : " + LevelManager.Instance.TargetScore;
        canvas.enabled = true;


        if(LevelManager.Instance.CurrentScore >= LevelManager.Instance.TargetScore)
        {
            AudioManager.Instance.PlaySound("WinSFX");
            Debug.Log("hi");
            if(SceneManager.sceneCountInBuildSettings - 1 >= SceneManager.GetActiveScene().buildIndex + 1)
            {
            Debug.Log("halo");
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
            Debug.Log("alo");
                nextLevelButton.gameObject.SetActive(false);
            }
            retryButton.gameObject.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlaySound("LoseSFX");
            nextLevelButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.sceneCountInBuildSettings - 1 <  nextSceneIndex)
        {
            return;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
