using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextRound : MonoBehaviour
{
    [SerializeField] private bool _canGoNextRound;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void OnClick()
    {
        if(_canGoNextRound == true)
        {
            NextRound();
        }
    }

    void NextRound()
    {
        //give new hand to the player
    }
}
