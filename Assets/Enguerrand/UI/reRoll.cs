using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class reRoll : MonoBehaviour
{
    [SerializeField] private Button _Dice;
    //[SerializeField] ;
    [SerializeField] private int _handStart = 5;
    [SerializeField] private bool _canReroll = true;
    
    void Start()
    {
        
    }

    
    void OnClick()
    {
        if(_canReroll = true)
        {
            Reroll();
        }
    }

    void Reroll()
    {
        /*foreach ()
        {
            //take the next hand
        }*/
    }
}
