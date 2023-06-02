using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Android;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;

public class CanvaManager : MonoBehaviour
{
    [Header("pause menu")]
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private bool _gamePaused = false;

    [Header("turn")]
    [SerializeField] private TextMeshProUGUI _turnNumber;
    [SerializeField] private GameObject _nextBtn;
    [SerializeField] private bool _canGoNextRound = true;
    [SerializeField] private int _currentRound;

    [Header("sounds")]
    [SerializeField] private Slider _musicSlider, _sfxSlider;

    public void OnClick()
    {
        if (_canGoNextRound)
        {
            //if the player touch the Next button on the screen
            _currentRound++;
            _turnNumber.text = "Turn : " + _currentRound;
        }
    }


    void Pause()
    {
        _pauseMenuUI.SetActive(false);
        _canGoNextRound = false;
        Time.timeScale = 0;
        _gamePaused = true;
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("level_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("level_3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("level_4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("level_5");
    }

    public void toggleMusic()
    {
        soundManager.Instance.ToggleMusic();
    }
    public void toggleSfx()
    {
        soundManager.Instance.ToggleSfx();
    }

    public void musicVolume()
    {
        soundManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void sfxVolume()
    {
        soundManager.Instance.SfxVolume(_sfxSlider.value);
    }
}

