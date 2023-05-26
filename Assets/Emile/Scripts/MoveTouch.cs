using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTouch : MonoBehaviour
{
    private Touch _touch;
    private bool _selected;
    private bool _onTile;
    //[SerializeField]GameObject grid
    void Update()
    {
        _touch = Input.GetTouch(0);
        Vector2 pos = Camera.main.ScreenToWorldPoint(_touch.position);
        Debug.Log(pos);
        Debug.Log(transform.position);
        if (_touch.phase == TouchPhase.Began && transform.position.x + 0.5 >= pos.x && pos.x >= transform.position.x - 0.5 && transform.position.y + 0.5 >= pos.y && pos.y >= transform.position.y - 0.5)
        {
            _selected = true;
        }
        if ((_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary) && _selected == true)
        {
            transform.position = pos;
        }
        if (_touch.phase == TouchPhase.Ended)
        {
            if (_onTile == true)
            {
                //fnc pour poser carte
            }
        }
        if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
        {
            _selected = false;
            _onTile = false;
        }
    }
}
