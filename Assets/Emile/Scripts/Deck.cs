using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private Hand _hand;
    [SerializeField] private List<Card> _deck;
    private int _pioche;
    private int _cardNbr;
    private void Start()
    {
    }

    // Start is called before the first frame update
    private void PickCard(int nbr)
    {
        for(int i = 0; i < nbr; i++)
        {
            _hand.Deck.Add(_deck[0]);
            _deck.Remove(_deck[0]);
        }
    }
}
