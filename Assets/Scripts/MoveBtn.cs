using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBtn : MonoBehaviour
{
    [SerializeField]RectTransform Door;
    void Update()
    {
        GetComponent<RectTransform>().position = new Vector2(Door.position.x, transform.position.y);
    }
}
