using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> bgmSounds;
    [SerializeField] private List<Sound> sfxSounds;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup sfxGroup, bgmGroup;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);

            }
            return instance;
        }
    }
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        
        foreach (Sound s in bgmSounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = bgmGroup;
        }

        foreach(Sound s in sfxSounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = sfxGroup;
        }
    }

    private Sound GetSound(string name)
    {
        Sound s = sfxSounds.Find((s) => s.Name == name);
        if (s == null)
        {
            s = bgmSounds.Find((s)=> s.Name == name);
        }
        return s;
    }

    public bool IsSoundPlaying(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return false;
        return s.Source.isPlaying;
    }

    public void PlaySound(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.pitch = 1f;
        s.Source.Play();
    }
    public void PlaySound(string name, Vector2 pitchRange)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        s.Source.Play();
    }

    public void PlaySoundOneShot(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.pitch = 1;
        s.Source.PlayOneShot(s.Clip);
    }

    public void StopSound(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.Stop();
    }
    public void PauseSound(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.Pause();
    }

    public void UnPauseSound(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.Source.UnPause();
    }

}
