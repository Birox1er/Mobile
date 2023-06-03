using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnResolution : MonoBehaviour
{
    private Chara[] all;
    [SerializeField] private EnnemiMoveSystem ennemies;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject UI;
    public bool turn;
    public void OnNextTurn()
    {
        
        if (turn == true)
        {
            turn = false;
            all = FindObjectsOfType<Chara>();
            TriInsertion(all);
            StartCoroutine(AttackTurn());
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
                sortArray[index] = sortArray[index-1];
                sortArray[index-1] = x; ;
                --index;
            }
        }
    }
    IEnumerator AttackTurn()
    {
;        for (int i = 0; i < all.Length; i++)
        {
            Debug.Log(i);
            List<Vector3> attack=new List<Vector3>();
            if (all[i] == null)
            {
                continue;
            }
            List<Chara> inRange = all[i].CheckInRange();
            if (inRange != null && inRange.Count != 0)
            {
                int cible = (int)Random.Range(0, inRange.Count);
                attack.Add(all[i].transform.position- (all[i].transform.position-inRange[cible].transform.position)/2);
                attack.Add(all[i].transform.position);
                if (all[i]._isUltOn)
                {
                    all[i].Ult();
                }
                else
                {
                    Debug.Log(inRange.Count);
                    all[i].Attack(inRange[cible]);
                }
                if (all[i].Classe1 != Chara.Classe.Archer)
                {
                    all[i].GetComponent<Unit>().MoveThroughPath(attack);
                }
                yield return new WaitForSeconds(1);
            }
        }
        if (GameObject.FindGameObjectsWithTag("Ennemi").Length == 0)
        {
            UI.SetActive(false);
            win.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            UI.SetActive(false);
            gameOver.SetActive(true);
        }
        ennemies.OnNextTurn();
        Unit[] units = FindObjectsOfType<Unit>();

        turn = true;
    }
}
