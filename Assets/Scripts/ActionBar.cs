using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private GameObject  posD;
    [SerializeField] private GameObject  posF;
     private Chara[] chara;    // Start is called before the first frame update
    [SerializeField] private List<Sprite> sprite;
    private List<Sprite> spr=new List<Sprite>();

    //transorm to StartGame or other function for finalGame;
    void Start()
    {
        
        chara = FindObjectsOfType<Chara>();
        for(int i = 0; i < chara.Length; i++)
        {
            switch (chara[i].Classe1)
            {
                case Chara.Classe.Archer:
                    spr.Add(sprite[0]);
                    break;
                case Chara.Classe.Warrior:
                    spr.Add(sprite[1]);
                    break;
                case Chara.Classe.Tank:
                    spr.Add(sprite[2]);
                    break;
                case Chara.Classe.Oni:
                    spr.Add(sprite[3]);
                    break;
                case Chara.Classe.Undead:
                    spr.Add(sprite[4]);
                    break;
                case Chara.Classe.Kappa:
                    spr.Add(sprite[5]);
                    break;
            }
            Vector3 pos=Vector3.Lerp(posD.transform.position,posF.transform.position,(float)chara[i].Prio/6);
 
            GameObject inst = Instantiate(new GameObject(),new Vector3( pos.x,pos.y,-1), transform.rotation,transform);
            inst.AddComponent<Image>();
            inst.GetComponent<Image>().sprite = spr[i];
            inst.transform.localScale = new Vector3(0.3f, 0.3f,0.3f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
