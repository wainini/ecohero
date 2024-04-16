using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private SettingsSaveData saveData;

    private void Awake()
    {
        StartCoroutine(WaitUntilDataAvailable());
    }

    private IEnumerator WaitUntilDataAvailable()
    {
        yield return new WaitUntil(() => SaveLoadManager.Instance?.GetSettingsData is not null);

        saveData = SaveLoadManager.Instance.GetSettingsData; //save the reference to be access/changed everytime setting is changed
        masterSlider.value = saveData.VolumeSaveData.MasterSliderValue;
        sfxSlider.value = saveData.VolumeSaveData.SFXSliderValue;
        bgmSlider.value = saveData.VolumeSaveData.BGMSliderValue;
        
    }

    public void SetMasterVol(float value)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(value) * 20);
        saveData.VolumeSaveData.MasterSliderValue = value;
    }

    public void SetSFXVol(float value)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
        saveData.VolumeSaveData.SFXSliderValue = value;
    }

    public void SetBGMVol(float value)
    {
        mixer.SetFloat("BGMVol", Mathf.Log10(value) * 20);
        saveData.VolumeSaveData.BGMSliderValue = value;
    }

    //public void SetResolution()
    //{

    //}

    //public void SetFullscreen()
    //{

    //}
}
