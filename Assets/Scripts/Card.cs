using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] GameObject prefabUnit;
    private bool isOnTile = false;
    private Vector3 basePos;
    [SerializeField]private Chara.Classe classe;
    [SerializeField]private List<Card> cards= new List<Card>();

    public bool IsOnTile { get => isOnTile; set => isOnTile = value; }
    public Vector3 BasePos { get => basePos; set => basePos = value; }
    public Chara.Classe Classe { get => classe; set => classe = value; }

    private void Start()
    {
        Card[] a = FindObjectsOfType<Card>();
        foreach (Card card in a)
        {
            if (card.Classe == Classe)
            {
                continue;
            }
            cards.Add(card);

        }
    }

    public bool IsOnOtherCard(Camera cam,Vector3 ray)
    {
        
        HexGrid grid = FindObjectOfType<HexGrid>();
        
        bool b=false;
        foreach(Card card in cards)
        {
            if (grid.GetClosestHex(ray) == grid.GetClosestHex(cam.ScreenToWorldPoint(card.transform.position)))
            {
                b = true;
                
            }
        }
        return b;
    }
    public void InitUnit()
    {
        GameObject chara = Instantiate(prefabUnit, BasePos, Quaternion.identity);
        chara.GetComponent<Chara>().Classe1 = classe;
        gameObject.SetActive(false);
    }
}
