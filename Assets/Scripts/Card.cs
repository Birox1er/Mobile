using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] GameObject prefabUnit;
    private bool isOnTile = false;
    private Vector3 basePos;
    [SerializeField]private Chara.Classe classe;

    public bool IsOnTile { get => isOnTile; set => isOnTile = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Chara.Classe Classe { get => classe; set => classe = value; }

    private void Start()
    {
        //basePos = transform.position;
    }


    public void InitUnit()
    {
        GameObject chara = Instantiate(prefabUnit, BasePos, Quaternion.identity);
        chara.GetComponent<Chara>().Classe1 = classe;
        gameObject.SetActive(false);
    }
}
