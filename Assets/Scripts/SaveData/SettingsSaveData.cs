[System.Serializable]
public class SettingsSaveData
{
    public VolumeSaveData VolumeSaveData;
    public ResolutionSaveData ResolutionSaveData;
}

[System.Serializable]
public class VolumeSaveData
{
    public float MasterSliderValue;
    public float SFXSliderValue;
    public float BGMSliderValue;
}

[System.Serializable]
public class ResolutionSaveData
{
    public int Width;
    public int Height;
    public bool Fullscreen;
}
