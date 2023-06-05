using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MoveTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Touch _touch;
    private bool _selected;
    private bool _onTile;
    //private int _mov;
    [SerializeField] private GameObject initPos1;
    [SerializeField] private GameObject initPos2;
    [SerializeField] private GameObject initPos3;
    private Camera mainCamera;
    public LayerMask mask;
    [SerializeField] private HexGrid grid;
    Vector3 b1;
    Vector3 b2;
    Vector3 b3;

EventSystem eventSystem;
    private GameObject targetSelected;

    //public int Mov { get => _mov; }
    private void Awake()
    {
        //_mov = gameObject.GetComponent<Chara>()._m((ov;
        if(mainCamera == null)
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
    }

    void Update()
    {
        if(Input.touchCount > 0)
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

                    if (Physics.Raycast(ray, Vector3.forward, out hit, 100, mask))
                    {
                        Debug.Log(hit.transform.name);
                        if(targetSelected.name == "unit_1" && hit == null)
                            targetSelected.transform.position = b1;

                        if(targetSelected.name == "unit_2")
                            targetSelected.transform.position = b2;

                        if (targetSelected.name == "unit_3")
                                targetSelected.transform.position = b3;
                    }
                    break;

                case TouchPhase.Ended:
                    if (_onTile == true)
                    {
                        //fnc pour poser carte
                        //Instantiate unit
                    }
                    break;

                case TouchPhase.Canceled:
                    _selected = false;
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
        targetSelected = null;
    }
}
