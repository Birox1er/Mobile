using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class World_Map : MonoBehaviour
{
    [Header("Level Info")]
    [SerializeField] string _levelName;

    [Header("Nodes Destinations")]
    [SerializeField] GameObject _previousDestination;
    [SerializeField] GameObject _nextDestination;

    [Header("Condition")]
    [SerializeField]public  bool _isLocked;
    //public bool _isLocked { get; private set ; }

    [Header("Dependencies")]
    [SerializeField] GameObject _player;
    [SerializeField] bool _currentNode;
    [SerializeField] Text levelNameText;
    [SerializeField] bool _menuActive;
    [SerializeField] GameObject playBtn;
    

    // Start is called before the first frame update
    void Start()
    {   
        if (_currentNode)
        {
            if (_isLocked)
            {
                if (_nextDestination != null)
                {
                    if (_nextDestination.GetComponent<World_Map>())
                    {
                        if (_nextDestination.GetComponent<World_Map>()._isLocked == false)
                        {
                            _nextDestination.SetActive(false);
                        }
                    }
                }
                else if (_previousDestination != null)
                {
                    if (_previousDestination.GetComponent<World_Map>())
                    {
                        if (_previousDestination.GetComponent<World_Map>()._isLocked == false)
                        {
                            _previousDestination.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            _isLocked = true;
            if (_nextDestination != null)
            {
                _nextDestination.SetActive(true);
                if (_nextDestination.GetComponent<World_Map>())
                {
                    _nextDestination.GetComponent<World_Map>()._isLocked = true;
                }
            }
            if (_previousDestination != null)
            {
                _previousDestination.SetActive(true);
                if (_previousDestination.GetComponent<World_Map>())
                {
                    _previousDestination.GetComponent<World_Map>()._isLocked = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == _player.transform.position)
        {
            _currentNode = true;
            //active ui niveeau 1
        }
        else
        {
            _currentNode = false;
            //deactivate ui niv 1
        }
        if(_currentNode)
        {
            //levelNameText.text = _levelName;

        }

    }

    public void rightBtn()
    {
        if(_nextDestination != null)
        {
            if (_nextDestination.activeInHierarchy)
            {
                _currentNode = false;
                //playBtn.GetComponent<Animator>().Play("playBtnExit", 0,0f);
                Invoke("playBtnDisable", 1 / 6f);
                StartCoroutine(DoRight());
            }
        }
    }

    public void leftBtn()
    {
        if(_previousDestination != null)
        {
            if (_previousDestination.activeInHierarchy)
            {
                _currentNode = false;
                //playBtn.GetComponent<Animator>().Play("playBtnExit", 0, 0f);
                Invoke("playBtnDisable", 1 / 6f);
                StartCoroutine(DoLeft());
            }
        }
    }

    void PlayBtnDisable()
    {
        playBtn.SetActive(false);
    }

    IEnumerator DoLeft()
    {
        yield return new WaitForSeconds(1/60);
        while(_player.transform.position != _previousDestination.transform.position)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _previousDestination.transform.position, 600f * Time.deltaTime);
            yield return null;
        }
        _player.GetComponent<Nodes_Navigation>()._Duration = true;
    }

    IEnumerator DoRight()
    {
        yield return new WaitForSeconds(1 / 60);
        while (_player.transform.position != _nextDestination.transform.position)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _nextDestination.transform.position, 600f * Time.deltaTime);
            yield return null;
        }
        _player.GetComponent<Nodes_Navigation>()._Duration = true;
    }

    

}
