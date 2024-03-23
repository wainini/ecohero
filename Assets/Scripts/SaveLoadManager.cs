using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private const string SettingsSaveDataFileName = "SettingsData.json";
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

    private void SaveData()
    {
        if(!Directory.Exists(settingsDataPath))
            Directory.CreateDirectory(settingsDataPath);

        string settingsDataJSON = JsonUtility.ToJson(settingsSaveData);
        File.WriteAllText(settingsDataPath + SettingsSaveDataFileName, settingsDataJSON);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
