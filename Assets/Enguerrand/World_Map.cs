using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World_Map : MonoBehaviour
{
    [Header("Nodes Destinations")]
/*    [SerializeField] GameObject _previousDestination;
    [SerializeField] GameObject _nextDestination;*/
    public GameObject goToDestination;
    public GameObject panelInfoDestination;

    [Header("Condition")]
    [SerializeField]public  bool _isLocked;
    //public bool _isLocked { get; private set ; }

    [Header("Dependencies")]
    [SerializeField] GameObject _player;
    [SerializeField] bool _currentNode;
    [SerializeField] bool _menuActive;
    [SerializeField] GameObject playBtn;
    

    // Start is called before the first frame update
    void Start()
    {
        if (_currentNode)
        {
            if (_isLocked)
            {
               /* if (_nextDestination != null)
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
                }*/
            }
        }
        else
        {
            _isLocked = true;
            /*if (_nextDestination != null)
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
            }*/
        }
    }

    /*public void rightBtn()
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
*/
    /*IEnumerator DoLeft()
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

        panelInfoDestination?.SetActive(true);
    }*/

    //--------------this parts is a test-------------

    public void destination()
    {
        if (goToDestination != null)
        {
            if (goToDestination.activeInHierarchy)
            {
                _currentNode = false;
                //playBtn.GetComponent<Animator>().Play("playBtnExit", 0, 0f);
                Invoke("playBtnDisable", 1 / 6f);
                StartCoroutine(GoToDestination());
            }
        }
    }

    IEnumerator GoToDestination()
    {
        yield return new WaitForSeconds(1 / 60);
        while(_player.transform.position != goToDestination.transform.position)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, goToDestination.transform.position, 600f*Time.deltaTime);
            yield return null;
        }
        _player.GetComponent<Nodes_Navigation>()._Duration = true;
        panelInfoDestination?.SetActive(true);
    }
}
