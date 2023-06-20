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
    public static bool Exc;

    [Header("sounds")]
    [SerializeField] private Button _musicBtn, _sfxBtn;

   
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
            //_turnNumber.text = "Turn : " + _currentRound;
        }
    }


    void Pause()
    {
        _pauseMenuUI.SetActive(false);
        _gamePaused = true;
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void PlacementUnit()
    {
        Exc = false;
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
        int i = 0;
        foreach(var unitSlot in FindObjectsOfType<Card>())
        {
            if (unitSlot.IsOnTile)
                unitSlot.InitUnit();
            if (i == 1)
            {
                Exc = true;
            }
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

