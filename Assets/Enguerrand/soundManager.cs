using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] public static soundManager Instance {get; private set;}
    [SerializeField] AudioClip _musicSound, _sfxSound;
    [SerializeField]  AudioSource _musicSource, _sfxSource;

    [Header("imgSwap")]
    [SerializeField] Sprite _newMusicSprite;
    [SerializeField] Sprite _currentMusciSprite;
    [SerializeField] Image _currentMusciImg;
    [SerializeField] Sprite _newSfxSprite;
    [SerializeField] Sprite _newCurrentSfxSprite;
    [SerializeField] Image _currentSfxImg;


    public void Start()
    {
        foreach(AudioSource audio in FindObjectsOfType<AudioSource>())
        {

            if (audio.CompareTag("Sfx"))
            {
                _sfxSource = audio;
                Debug.Log("&hh");
            }
            if (audio.CompareTag("Music"))
            {
                _musicSource = audio;
                Debug.Log("&hh");
            }
        }
        if(PlayerPrefs.GetInt("audio") == 1)
        {
            _musicSource.mute=true;
            if (_currentMusciImg != null)
                _currentSfxImg.sprite = _newSfxSprite;
        }
        else
        {
            _musicSource.mute = false;
            if(_currentMusciImg != null)
            _currentSfxImg.sprite = _newCurrentSfxSprite;
        }
        if (PlayerPrefs.GetInt("sfx") == 1)
        {
            _sfxSource.mute = true;
            if (_currentMusciImg != null)
                _currentMusciImg.sprite = _newMusicSprite;
        }
        else
        {
            if (_currentMusciImg != null)
                _currentMusciImg.sprite = _currentMusciSprite;
            _sfxSource.mute = false;
        }
    }
    public  void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    public void PlayOneShot(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public  void PlaySfx(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void ToggleMusic()
    {

        if( PlayerPrefs.GetInt("audio")==0)
        {
            _musicSource.mute = true;
            _currentMusciImg.sprite = _newMusicSprite ;
            PlayerPrefs.SetInt("audio", 1);
        }
        else
        {
            _musicSource.mute = false;
            _currentMusciImg.sprite = _currentMusciSprite;
            PlayerPrefs.SetInt("audio", 0);
        }
    }
    public void ToggleSfx()
    {
       
        if(PlayerPrefs.GetInt("sfx", 0) == 0)
        {
            _musicSource.mute = true;
            _currentSfxImg.sprite = _newSfxSprite;
            PlayerPrefs.SetInt("sfx", 1);
        }
        else
        {
            _sfxSource.mute = false;
            _currentSfxImg.sprite = _newCurrentSfxSprite ;
            PlayerPrefs.SetInt("sfx", 0);
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
