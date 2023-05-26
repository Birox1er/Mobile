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

    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
        SceneManager.LoadScene("Enguerrand");
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }
}

