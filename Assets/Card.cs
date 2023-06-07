using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] GameObject prefabUnit;
    private bool isOnTile = false;
    private Vector3 basePos;

    public bool IsOnTile { get => isOnTile; set => isOnTile = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    private void Start()
    {
        //basePos = transform.position;
    }


    public void InitUnit()
    {
        Instantiate(prefabUnit, BasePos, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
