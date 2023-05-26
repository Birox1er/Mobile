using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTouch : MonoBehaviour
{
    private Touch _touch;
    private bool _selected;
    private bool _onTile;
    //private int _mov;

    //public int Mov { get => _mov; }
    private void Start()
    {
        //_mov = gameObject.GetComponent<Chara>()._mov;
    }
    //[SerializeField]GameObject grid
    void Update()
    {
        _touch = Input.GetTouch(0);
        Vector2 pos = _touch.position;
        Debug.Log(pos);
        Debug.Log(transform.position);
        if (_touch.phase == TouchPhase.Began && transform.position.x + 80 >= pos.x && pos.x >= transform.position.x - 80 && transform.position.y + 80 >= pos.y && pos.y >= transform.position.y - 80)
        {
            Debug.Log("aaa");
            _selected = true;
        }
        if ((_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary) && _selected == true)
        {
            Debug.Log("aaa2");
            transform.position = pos;
        }
        if (_touch.phase == TouchPhase.Ended)
        {
            Debug.Log("aaa3");
            if (_onTile == true)
            {
                Debug.Log("aaa4");
                //fnc pour poser carte
            }
        }
        if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
        {
            Debug.Log("aaa6");
            _selected = false;
            _onTile = false;
        }
    }
}
