using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        MenuManager.Instance.OpenMenuOverlap("SettingsMenu");
        print("at?");
    }
    // Update is called once per frame
    public void ResumeGame()
    {
        MenuManager.Instance.CloseMenu();
    }
}
