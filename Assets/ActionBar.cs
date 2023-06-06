using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private GameObject  posD;
    [SerializeField] private GameObject  posF;
     private Chara[] chara;    // Start is called before the first frame update
    [SerializeField] private GameObject sprite;
    //transorm to StartGame or other function for finalGame;
    void Start()
    {
        chara = FindObjectsOfType<Chara>();
        for(int i = 0; i < chara.Length; i++)
        {
            Vector3 pos=Vector3.Lerp(posD.transform.position,posF.transform.position,(float)chara[i].Prio/6);
 
            Instantiate(sprite,new Vector3( pos.x,pos.y,-1), transform.rotation,transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
