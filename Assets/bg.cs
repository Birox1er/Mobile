using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg : MonoBehaviour
{
    void update()
    {

        //writing this code in update only to see the changes on the run :P
        float dimension = 0;
        float width = Screen.width;
        float length = Screen.height;
        if (width > length)
            dimension = width;
        else
            dimension = length;

        transform.localScale = new Vector3(dimension, dimension, dimension);
    }
}
