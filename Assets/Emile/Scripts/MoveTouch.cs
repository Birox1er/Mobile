using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MoveTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static MoveTouch Instance { get; private set; }

    [Header("card's information")]
    [SerializeField] private GameObject initPos1;
    [SerializeField] private GameObject initPos2;
    [SerializeField] private GameObject initPos3;
    [SerializeField] private GameObject Card_1;
    [SerializeField] private GameObject Card_2;
    [SerializeField] private GameObject Card_3;
    [SerializeField] private HexGrid grid;

    [Header("btn")]
    [SerializeField] private GameObject playbtn;

    private Camera mainCamera;
    public LayerMask mask;
    private Touch _touch;
    private bool _selected;
    private bool _onTile;
    private bool _onTile1;
    private bool _onTile2;
    private bool _onTile3;
    private bool _canPressPlay;
    Vector3 b1;
    Vector3 b2;
    Vector3 b3;

    EventSystem eventSystem;
    private GameObject targetSelected;

    private bool _hexDetected;
    //public int Mov { get => _mov; }
    private void Awake()
    {
        Instance ??= this;

        //_mov = gameObject.GetComponent<Chara>()._m((ov;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        eventSystem = EventSystem.current;
        b1 = initPos1.transform.position;
        b2 = initPos2.transform.position;
        b3 = initPos3.transform.position;
        grid = FindObjectOfType<HexGrid>();
    }

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            Vector3 pos = _touch.position;

            /*        Debug.Log(pos);
                    Debug.Log(transform.position);*/

            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    if (targetSelected != null)
                    {
                        _selected = true;
                        //if (transform.position.x + 80 >= pos.x && pos.x >= transform.position.x - 80 && transform.position.y + 80 >= pos.y && pos.y >= transform.position.y - 80)
                        
                    }
                    break;

                case TouchPhase.Moved:

                case TouchPhase.Stationary:
                    if (!_selected)
                        return;

                    targetSelected.transform.position = pos;
                    RaycastHit hit;
                    Vector3 ray = mainCamera.ScreenToWorldPoint(pos);

                    _hexDetected = Physics.Raycast(ray, Vector3.forward, out hit, 100, mask);
                    if (_hexDetected)
                    {
                        //Debug.Log(hit.transform.name);
                        if(hit.transform.name == "Hex(Clone)")
                        {
                            _onTile = true;
                            if(targetSelected == Card_1)
                                _onTile1 = true;
                            if(targetSelected == Card_2)
                                _onTile2 = true;
                            if(targetSelected == Card_3)
                                _onTile3 = true;
                        }
                    }
                    
                    break;

                case TouchPhase.Ended:
                    if (_onTile == true)
                    {
                        //Debug.Log(mainCamera.ScreenToWorldPoint(targetSelected.transform.position));
                        Vector3Int ahh = grid.GetClosestHex(mainCamera.ScreenToWorldPoint(targetSelected.transform.position));
                        Debug.Log(ahh);
                        Vector3 bhh = new Vector3(ahh.x * 1.73f, ahh.y * 2, targetSelected.transform.position.z);
                        Debug.Log(bhh);
                        targetSelected.transform.position = mainCamera.WorldToScreenPoint(bhh);

                        //condition to pressbtn play
                        if (_onTile1 == true && _onTile2 == true && _onTile3 == true)
                        {
                            playbtn.SetActive(true);

                        }
                        _onTile= false;
                    }
                    else
                    {
                        if (!_hexDetected)
                        {
                            if (targetSelected == Card_1)
                            {
                                targetSelected.transform.position = b1;
                                _onTile1= false;
                            }
                            if (targetSelected == Card_2)
                            {
                                targetSelected.transform.position = b2;
                                _onTile2 = false;
                            }
                            if (targetSelected == Card_3)
                            {
                                targetSelected.transform.position = b3;
                                _onTile3 = false;
                            }
                        }
                    }

                   
                    //--------------------test---------------------\\
                    /*if (Card_1.transform.position != b1 && Card_2.transform.position != b2 && Card_3.transform.position != b3)
                    {
                        playbtn.SetActive(true);

                    }*/

                    targetSelected = null;

                    break;

                case TouchPhase.Canceled:
                    _selected = false;
                    _onTile1 = false;
                    _onTile2 = false;
                    _onTile3 = false;
                    _onTile = false;
                    break;
            }
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetSelected = eventData.pointerEnter;

        Debug.Log(targetSelected.transform.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
