using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes_Navigation : MonoBehaviour
{
    [Header("Nodes")]
    [SerializeField] private World_Map[] nodes;
    [SerializeField] private World_Map currentNode;
    private int i;
    public bool _Duration = true;


    void Awake()
    {
        currentNode = nodes[0];
        i = 0;
    }
    
    public void RightBtn()
    {
        if (i != nodes.Length - 1 && !nodes[i+1]._isLocked && _Duration ==true) 
        {
            _Duration = false;
            currentNode.rightBtn();
            i++;
            currentNode = nodes[i];

        }
    }

    public void LeftBtn()
    {
        if(i!=0 && _Duration == true) 
        {
            _Duration=false;
            currentNode.leftBtn();
            i--;
            currentNode = nodes[i];
        }
    }
}
