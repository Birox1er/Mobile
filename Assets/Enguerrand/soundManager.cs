using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] public static soundManager Instance {get; private set;}
    [SerializeField] AudioClip _musicSound, _sfxSound;
    [SerializeField] AudioSource _musicSource, _sfxSource;

    [Header("imgSwap")]
    [SerializeField] Sprite _newMusicSprite;
    [SerializeField] Sprite _currentMusciSprite;
    [SerializeField] Image _currentMusciImg;
    [SerializeField] Sprite _newSfxSprite;
    [SerializeField] Sprite _newCurrentSfxSprite;
    [SerializeField] Image _currentSfxImg;

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

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
        if(_musicSource.mute )
        {
            _currentMusciImg.sprite = _currentMusciSprite;
        }
        else
        {
            _currentMusciImg.sprite = _newMusicSprite;
        }
    }
    public void ToggleSfx()
    {
        _sfxSource.mute = !_sfxSource.mute;
        if(_sfxSource.mute )
        {
            _currentSfxImg.sprite = _newCurrentSfxSprite;
        }
        else
        {
            _currentSfxImg.sprite = _newSfxSprite;
        }
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
