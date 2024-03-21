using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    private SettingsSaveData settingsSaveData = null;
    public SettingsSaveData GetSettingsData { get { return settingsSaveData; } }

    private string settingsDataPath;

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        settingsDataPath = Application.persistentDataPath + "/SettingsSaveData/";

        LoadSettingsData();
    }

    private void LoadSettingsData()
    {
        if(File.Exists(settingsDataPath))
        {
            string dataJSON = File.ReadAllText(settingsDataPath);
            settingsSaveData = JsonUtility.FromJson<SettingsSaveData>(dataJSON);
        }
        else //there're no saved data 
        {
            settingsSaveData = null;
        }
    }

    private void SaveData()
    {
        string settingsDataJSON = JsonUtility.ToJson(settingsSaveData);
        File.WriteAllText(settingsDataPath, settingsDataJSON);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
