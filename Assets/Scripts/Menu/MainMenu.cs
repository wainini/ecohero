using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string settingsMenuName = "SettingsMenu";

    public void StartGame()
    {
        //starts game
    }

    public void OpenSettings()
    {
        MenuManager.Instance.OpenMenuOverlap(settingsMenuName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
