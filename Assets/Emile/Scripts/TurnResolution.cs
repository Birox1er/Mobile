using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;



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
    private HexGrid grid;
    public bool turn;
    private int nbrEnemi = 0;
    private int deadEnemi = 0;
    [SerializeField] private Camera cam;
    private void Start()
    {
        grid = FindObjectOfType<HexGrid>();
        cam = Camera.main;
    }

    public UnitManager UM { get => uM; }

    public void OnNextTurn()
    {
        if (turn == true)
        {
            uM.PlayersTurn = false;
            turn = false;
            all = FindObjectsOfType<Chara>();
            int j = 0;
            foreach(Chara chara in all)
            {
                if (!chara.Allied)
                {
                    j++;
                }
            }
            if (j> nbrEnemi)
            {
                nbrEnemi = j;
            }
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
                if (cam.GetComponent<FixCamera>().isZoomed == false) {
                    cam.GetComponent<FixCamera>().ZoomToTarget(all[i].transform);
                }
                if (all[i].Classe1 == Chara.Classe.Archer || all[i].Classe1 == Chara.Classe.Kappa) 
                {
                    GameObject projectile = Instantiate(new GameObject(), all[i].transform.position, all[i].transform.rotation);
                    projectile.AddComponent<SpriteRenderer>();
                    projectile.GetComponent<SpriteRenderer>().sprite = all[i].Prj;
                    projectile.AddComponent<Projectile>();
                    projectile.GetComponent<Projectile>().Prj(inRange[cible].transform.position);
                    cam.GetComponent<FixCamera>().FollowTarget(projectile.transform);
                }
                else
                {
                    cam.GetComponent<FixCamera>().FollowTarget(all[i].transform);
                }
                all[i].Attack(inRange[cible]);
                all[i].transform.position = new Vector3(all[i].transform.position.x, all[i].transform.position.y, -0.5f);
                if (inRange[cible].GetCurrentHealth() <= 0)
                {
                    all[i].Killed += 1;
                    deadEnemi += 1;
                    if (all[i].Allied && all[i].InForest)
                    {
                        GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQAg");
                    }
                    if (all[i].Classe1 == Chara.Classe.Warrior)
                    {
                        GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQBQ");
                    }
                    if (all[i].Classe1==Chara.Classe.Archer&&(grid.GetClosestHex(inRange[i].transform.position)-grid.GetClosestHex(all[i].transform.position)).magnitude>=all[i].RangeMax){
                        GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQDw");
                    }
                    if (inRange[cible].Classe1 == Chara.Classe.Kappa && nbrEnemi > 2 && deadEnemi == 1)
                    {
                        GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQDg");
                    }
                    yield return new WaitUntil(()=> inRange[cible].Dead);
                }
                else
                {
                    yield return new WaitForSeconds(3);
                }
                
            }
            if (all[i].Classe1 == Chara.Classe.Archer)
            {
                all[i].ArcherCacResolve();
            }
            if (all[i].Killed == nbrEnemi&&all[i].Classe1==Chara.Classe.Archer)
            {
                GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQBw");
            }
            if (all[i].Killed == nbrEnemi && all[i].Classe1 == Chara.Classe.Warrior)
            {
                GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQBQ");
            }
        }
        cam.GetComponent<FixCamera>().DezoomAndReset();
        if (GameObject.FindGameObjectsWithTag("Ennemi").Length == 0)
        {
            if(FindObjectsOfType<Chara>().Length==1&& FindObjectOfType<Chara>().Classe1 == Chara.Classe.Tank)
            {
                GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQBg");
            }
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
        int j = 0;
        for (int i = 0; i < unit.Length; i++)
        {
            if (unit[i].GetComponent<Chara>().Allied)
            {
                j++;
            }
            unit[i].GetComponent<Unit>().SetHasMoved(false);
        }
        if (j == 1)
        {
            GglManager.HandleAchievemen("CgkIsfzlyYQEEAIQEA");
        }
        nextTurn.interactable=true;
        turn = true;
        uM.PlayersTurn = true;
        
    }
}
