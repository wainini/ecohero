using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    public bool Loop;
    [Range(0,2)]
    public float Volume;
    public AudioSource Source;
}
