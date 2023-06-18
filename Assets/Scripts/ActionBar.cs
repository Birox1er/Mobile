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
    [SerializeField]private Sprite nAtk;
    [SerializeField] private Sprite dead;
    List<GameObject> Dead = new List<GameObject>();
    List<GameObject> nAtkGO= new List<GameObject>();
    float size;
    int prioOffset;

    //transorm to StartGame or other function for finalGame;
    void Start()
    {
        size = 0.3f;

        prioOffset = 0;
        chara = FindObjectsOfType<Chara>();
        if (chara.Length > 10)
        {
            size = size - 1 / chara.Length;
        }
        TriInsertion(chara);
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
            Vector3 pos=Vector3.Lerp(posD.transform.position,posF.transform.position,i/(float)chara.Length);
            Debug.Log((chara[i].Prio + prioOffset) / (float)chara.Length);
            GameObject inst = Instantiate(new GameObject(),new Vector3( pos.x,pos.y,-1), transform.rotation,transform);
            inst.AddComponent<Image>();
            inst.GetComponent<Image>().sprite = spr[i];
            inst.transform.localScale = new Vector3(size, size,size);
            GameObject inst2 = Instantiate(new GameObject(), new Vector3(pos.x, pos.y+40, -1), transform.rotation, transform);
            inst2.AddComponent<Image>();
            inst2.GetComponent<Image>().sprite = nAtk;
            inst2.transform.localScale = new Vector3(size, size, size);
            inst2.SetActive(false);
            GameObject inst3 = Instantiate(new GameObject(), new Vector3(pos.x, pos.y, -1), transform.rotation, transform);
            inst3.AddComponent<Image>();
            inst3.GetComponent<Image>().sprite = dead;
            inst3.transform.localScale = new Vector3(size, size, size);
            inst3.SetActive(false);
            nAtkGO.Add(inst2);
            Dead.Add(inst3);
        }
    }
    public void SetNAtkActive(Chara chars,bool isAcive)
    {
        int i= 0;
        
        foreach(Chara chare in chara)
        {
            if (chare == chars)
            {
                nAtkGO[i].SetActive(isAcive);
            }
            i++;
        }
    }
    public void SetDead(Chara chars)
    {
        int i = 0;

        foreach (Chara chare in chara)
        {
            if (chare == chars)
            {
                nAtkGO[i].SetActive(false);
                Dead[i].SetActive(true);
            }
            i++;
        }
    }
    public static void TriInsertion(Chara[] sortArray)
    {
        int tmp;
        for (int i = 1; i < sortArray.Length; ++i)
        {
            tmp = sortArray[i].Prio;
            int index = i;
            while (index > 0 && tmp < sortArray[index - 1].Prio)
            {
                Chara x = sortArray[index];
                sortArray[index] = sortArray[index - 1];
                sortArray[index - 1] = x; ;
                --index;
            }
        }
    }
}
