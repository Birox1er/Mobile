using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnResolution : MonoBehaviour
{
    private Chara[] all;
    private Unit[] unit;
    [SerializeField] private EnnemiMoveSystem ennemies;
    [SerializeField] private UnitManager uM;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject UI;
    [SerializeField] private Button nextTurn;
    [SerializeField] private Button reset;
    public bool turn;

    public UnitManager UM { get => uM; }

    public void OnNextTurn()
    {
        Debug.Log(turn);
        if (turn == true)
        {
            uM.PlayersTurn = false;
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
       for (int i = 0; i < all.Length; i++)
        {
            if (all[i] == null)
            {
                continue;
            }
            List<Chara> inRange = all[i].CheckInRange();
            if (inRange != null && inRange.Count != 0&&all[i].canAtk)
            { 
                int cible = (int)Random.Range(0, inRange.Count);
                all[i].Attack(inRange[cible]);
                all[i].transform.position = new Vector3(all[i].transform.position.x, all[i].transform.position.y, -0.5f);
                if (inRange[cible].GetCurrentHealth() <= 0)
                {
                    yield return new WaitUntil(()=> inRange[cible].Dead);
                }
                else
                {
                    yield return new WaitForSeconds(1);
                }
                
            }
            if (all[i].Classe1 == Chara.Classe.Archer)
            {
                all[i].ArcherCacResolve();
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
        unit = FindObjectsOfType<Unit>();
        for (int i = 0; i < unit.Length; i++)
        {
            unit[i].GetComponent<Unit>().SetHasMoved(false);
        }
        nextTurn.interactable=true;
        turn = true;
        uM.PlayersTurn = true;
        
    }
}
