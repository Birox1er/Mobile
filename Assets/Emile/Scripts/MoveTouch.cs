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
    [SerializeField] private List<GameObject> CardList;
    [SerializeField] private HexGrid grid;

    [Header("btn")]
    [SerializeField] private GameObject playbtn;

    private Camera mainCamera;
    public LayerMask mask;
    private Touch _touch;
    private bool _selected;
    private bool _onTile;
    private bool _canPressPlay;


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
        grid = FindObjectOfType<HexGrid>();


        CardList[0].transform.position = initPos1.transform.position;
        CardList[1].transform.position = initPos2.transform.position;
        CardList[2].transform.position = initPos3.transform.position;

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

                    _onTile = false;

                    targetSelected.transform.position = pos;
                    RaycastHit hit;
                    Vector3 ray = mainCamera.ScreenToWorldPoint(pos);

                    _hexDetected = Physics.Raycast(ray, Vector3.forward, out hit, 100, mask);
                    
                    if (_hexDetected&&hit.collider.CompareTag("unitSLot"))
                    {
                        //Debug.Log(hit.transform.name);
                        if(hit.transform.name == "Hex(Clone)")
                        {
                            _onTile = true;
                            targetSelected.GetComponent<Card>().IsOnTile=true;
                        }
                    }


                    break;

                case TouchPhase.Ended:
                    if (_onTile == true)
                    {
                        //Debug.Log(mainCamera.ScreenToWorldPoint(targetSelected.transform.position));
                        Vector3Int ahh = grid.GetClosestHex(mainCamera.ScreenToWorldPoint(targetSelected.transform.position));
                        Vector3 bhh;
                        if (ahh.x % 2 == 1)
                        {
                            bhh = new Vector3(ahh.x * 1.73f, ahh.y*2, targetSelected.transform.position.z - 1);
                        }
                        else
                        {
                            bhh = new Vector3(ahh.x * 1.73f, ahh.y * 2-1, targetSelected.transform.position.z - 1);
                        }
                        targetSelected.transform.position = mainCamera.WorldToScreenPoint(bhh);
                        bool allOnTile=true;
                        //condition to pressbtn play
                        foreach(GameObject cards in CardList)
                        {
                            Card card = cards.GetComponent<Card>();
                            if (!card.IsOnTile)
                            {
                                allOnTile=false;
                            }
                        }
                        if (allOnTile)
                        {
                            playbtn.SetActive(true);

                        }

                        targetSelected.GetComponent<Card>().BasePos = bhh;

                        _onTile = false;
                    }
                    else
                    {

                            foreach (GameObject cards in CardList)
                            {
                                if (targetSelected == cards)
                                {
                                    targetSelected.GetComponent<Card>().IsOnTile = false;
                                    targetSelected.transform.position = initPos1.transform.position;

                                    switch (targetSelected.name)
                                    {
                                        case "unit_1": targetSelected.transform.position = initPos1.transform.position; break;
                                        case "unit_2": targetSelected.transform.position = initPos2.transform.position; break;
                                        case "unit_3": targetSelected.transform.position = initPos3.transform.position; break;
                                    }
                                }
                            }
                        
                    }

                    targetSelected = null;

                    break;

                case TouchPhase.Canceled:
                    _selected = false;
                    foreach (GameObject cards in CardList)
                    {
                        cards.GetComponent<Card>().IsOnTile = false;
                    }
                    _onTile = false;
                    break;
            }
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetSelected = eventData.pointerEnter;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
    public void Instanciation()
    {

    }
}
