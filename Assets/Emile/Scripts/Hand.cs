using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private List<Card> _deck;
    [SerializeField] private int _handMax;
    private int _hand;

    public List<Card> Deck { get => _deck; set => _deck = value; }
    public int HandMax { get => _handMax; set => _handMax = value; }
    public int Hande { get => _hand = _deck.Count; private set=>_hand=_deck.Count; }

}
