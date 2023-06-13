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
    [SerializeField] private GameObject _nextTurnUI;
    [SerializeField] private GameObject _nextBtnUI;
    [SerializeField] private GameObject _helpBtnUI;
    [SerializeField] private GameObject _cardHolder;
    [SerializeField] private GameObject _resetBtnUI;

    [SerializeField] private bool _gamePaused = false;

    [Header("turn")]
    [SerializeField] private TextMeshProUGUI _turnNumber;
    //[SerializeField] private GameObject _nextBtn;
    [SerializeField] private bool _canGoNextRound = true;
    [SerializeField] private int _currentRound;
    [SerializeField] private TurnResolution can;


    [Header("sounds")]
    [SerializeField] private Button _musicBtn, _sfxBtn;

    [Header("chara")]
    [SerializeField] private GameObject chara;
   
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;

    }
    public void OnClick()
    {
        _canGoNextRound = can.turn;
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

    public void LoadTuto1()
    {
        SceneManager.LoadScene("Tuto1");
    }
    public void LoadTuto2()
    {
        SceneManager.LoadScene("Tuto2");
    }
    public void LoadTuto3()
    {
        SceneManager.LoadScene("Tuto3");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level_4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level_5");
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene("Level_6");
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene("Level_7");
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene("Level_8");
    }
    public void LoadLevel9()
    {
        SceneManager.LoadScene("Level_9");
    }

    public void PlacementUnit()
    {
        GlowMov[] setHex = FindObjectsOfType<GlowMov>();
        foreach(GlowMov hex in setHex)
        {
            if (hex.gameObject.CompareTag("unitSLot"))
            {
                hex.RemoveGlow();
            }
        }
        _pauseMenuUI.SetActive(true);
        _nextTurnUI.SetActive(true);
        _nextBtnUI.SetActive(true);
        _helpBtnUI.SetActive(true);
        _resetBtnUI.SetActive(true);
        foreach(var unitSlot in FindObjectsOfType<Card>())
        {
            unitSlot.InitUnit();
        }
        _cardHolder.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void toggleMusic()
    {
        soundManager.Instance.ToggleMusic();
    }
    public void toggleSfx()
    {
        soundManager.Instance.ToggleSfx();
    }

    /*public void musicVolume()
    {
        soundManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void sfxVolume()
    {
        soundManager.Instance.SfxVolume(_sfxSlider.value);
    }*/
}

