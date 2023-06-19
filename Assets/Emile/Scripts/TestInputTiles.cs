using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestInputTiles : MonoBehaviour
{
    public UnityEvent<Vector3> pointerPress;
    private Touch _touch;
    // Start is called before the first frame update
    void Update()
    {
        DetectPress();
        //DetectMouseClick();
    }

    /*private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            pointerPress?.Invoke(pos);
        }
    }*/

    public void DetectPress()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                Vector3 pos = _touch.position;
                pointerPress?.Invoke(pos);
            }
        }
    }


}
