using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoadManager : MonoBehaviour
{
    public UnityEvent<string> OnItemUnlocked;

    private const string SettingsSaveDataFileName = "SettingsData.json";
    private const string GameSaveDataFileName = "GameData.json";
    private static SaveLoadManager instance;
    public static SaveLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoadManager>();
                DontDestroyOnLoad(instance.gameObject);

            }
            return instance;
        }
    }

    private SettingsSaveData settingsSaveData = null;
    public SettingsSaveData GetSettingsData { get { return settingsSaveData; } }

    private GameSaveData gameSaveData = null;
    public GameSaveData GetGameSaveData { get { return gameSaveData; } }

    private string settingsDataPath;
    private string gameDataPath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        settingsDataPath = Application.persistentDataPath + "/SettingsSaveData/";
        gameDataPath = Application.persistentDataPath + "/GameSaveData/";

        LoadSettingsData();
        LoadGameData();
    }

    private void LoadSettingsData()
    {
        if(File.Exists(settingsDataPath + SettingsSaveDataFileName))
        {
            string dataJSON = File.ReadAllText(settingsDataPath + SettingsSaveDataFileName);
            settingsSaveData = JsonUtility.FromJson<SettingsSaveData>(dataJSON);
        }
        else //there're no saved data 
        {
            settingsSaveData = new();
            settingsSaveData.VolumeSaveData = new();
            settingsSaveData.ResolutionSaveData = new();
            settingsSaveData.VolumeSaveData.MasterSliderValue = 0.75f;
            settingsSaveData.VolumeSaveData.SFXSliderValue = 0.75f;
            settingsSaveData.VolumeSaveData.BGMSliderValue = 0.75f;
            settingsSaveData.ResolutionSaveData.Fullscreen = true;
            settingsSaveData.ResolutionSaveData.Width = 1920;
            settingsSaveData.ResolutionSaveData.Height = 1080;
        }
    }

    private void LoadGameData()
    {
        if (File.Exists(gameDataPath + GameSaveDataFileName))
        {
            string dataJSON = File.ReadAllText(gameDataPath + GameSaveDataFileName);
            gameSaveData = JsonUtility.FromJson<GameSaveData>(dataJSON);
        }
        else //there're no saved data 
        {
            gameSaveData = new GameSaveData();
        }
    }

    private void SaveData()
    {
        if(!Directory.Exists(settingsDataPath))
            Directory.CreateDirectory(settingsDataPath);

        string settingsDataJSON = JsonUtility.ToJson(settingsSaveData);
        File.WriteAllText(settingsDataPath + SettingsSaveDataFileName, settingsDataJSON);


        if (!Directory.Exists(gameDataPath))
            Directory.CreateDirectory(gameDataPath);

        string gameDataJSON = JsonUtility.ToJson(gameSaveData);
        File.WriteAllText(gameDataPath + GameSaveDataFileName, gameDataJSON);
    }

    public void AddUnlockedItemSaveData(string itemName)
    {
        if(gameSaveData == null)
        {
            gameSaveData = new();
        }
        if (gameSaveData.ListUnlockedItem.Contains(itemName)) return;

        gameSaveData.ListUnlockedItem.Add(itemName);

        OnItemUnlocked?.Invoke(itemName);
    }

    private void OnDisable()
    {
        OnItemUnlocked.RemoveAllListeners();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
