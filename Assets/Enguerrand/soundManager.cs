using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    [SerializeField] public static soundManager Instance {get; private set;}
    [SerializeField] Sound[] _musicSound, _sfxSound;
    [SerializeField] AudioSource _musicSource, _sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    void PlayMusic(string name)
    {
        Sound s = Array.Find(_musicSound, x => x.name == name);
        if(s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            _musicSource.clip = s.clip;
            _musicSource.Play();
        }
    }

    void PlayeSfx(string name)
    {
        Sound s = Array.Find(_musicSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sfx not found");
        }
        else
        {
            _sfxSource.clip = s.clip;
            _sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
    public void ToggleSfx()
    {
        _sfxSource.mute = !_sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        _sfxSource.volume = volume;
    }
}
