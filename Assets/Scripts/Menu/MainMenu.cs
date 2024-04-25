using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string settingsMenuName = "SettingsMenu";
    [SerializeField] private string levelSelectMenuName = "LevelSelectMenu";
    public void StartGame()
    {
        MenuManager.Instance.OpenMenu(levelSelectMenuName);
    }

    public void OpenSettings()
    {
        MenuManager.Instance.OpenMenuOverlap(settingsMenuName);
    }

    public void QuitGame()
    {
        print("qut");
        Application.Quit();
    }
}
