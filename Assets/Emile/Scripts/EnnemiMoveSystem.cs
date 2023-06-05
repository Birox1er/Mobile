using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnnemiMoveSystem : MonoBehaviour
{
    private BFSResult movRange;
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    private HexGrid grid;
    private List<Vector3Int> unitList = new List<Vector3Int>();
    [SerializeField] TurnResolution tr;

    public List<Vector3Int> FindUnit()
    {
        List<Vector3Int> units = new List<Vector3Int>();
        GameObject[] unit = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < unit.Length; i++)
        {
            units.Add(grid.GetClosestHex(unit[i].transform.position));
        }
        return units;
    }


    public List<Vector3Int> FindLowerHpUnit()
    {
        List<Vector3Int> units = new List<Vector3Int>();
        GameObject[] unit = GameObject.FindGameObjectsWithTag("Unit");
        GameObject lowerUnit = unit[0];
        for (int i = 1; i < unit.Length; i++)
        {
            if (unit[i].GetComponent<Chara>().GetCurrentHealth() < lowerUnit.GetComponent<Chara>().GetCurrentHealth())
            {
                lowerUnit = unit[i];
            }

        }
        units.Add(grid.GetClosestHex(lowerUnit.transform.position));
        return units;
    }


    private void MovRange(GameObject units)
    {
       movRange= GraphSearch.BFSGetRange(grid, grid.GetClosestHex(units.transform.position),100);
    }

    public void GetPathKappa(Vector3Int selectedHexPos, HexGrid grid)
    {
        //Get path to a tile at two tile from the selectedHexPos

    }


    public void GetPath(Vector3Int selectedHexPos, HexGrid grid)
    {
        //movRange.GetRangePos().ToList().ForEach(x => Debug.Log(x));
        if (movRange.GetRangePos().ToList().Exists(x => x .Equals(selectedHexPos)))
        {
            
            currentPath = movRange.GetPathTo(selectedHexPos);
        }
    }


    public void MoveUnit(Unit selectedUnit, HexGrid grid)
    {

        var t = currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList();
   
        if (currentPath.Count > 1)
        {
            selectedUnit.MoveThroughPathE(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList(), selectedUnit.Mov);
        }
        
    }


    public void OnNextTurn()
    {
        grid = FindObjectOfType<HexGrid>();
        StartCoroutine(MovEnemy());
    }


    public void FirstTurn()
    {
        OnNextTurn();
    }


    private void Oni(GameObject unit, HexGrid grid)
    {

        bool isClose = false;
        unitList = FindUnit();
        List<Vector3Int> neibourgh = grid.GetNeighbours(grid.GetClosestHex(unit.transform.position));

        foreach (Vector3Int units in unitList)
        {
            foreach (Vector3Int neibourghs in neibourgh)
            {
                if (units == neibourghs)
                {
                    isClose = true;
                }
            }

            
        }
        if (isClose == false)
        {
            unitList.Clear();
            unitList = FindLowerHpUnit();


            MovRange(unit);
            if (currentPath.Count <= 0 || currentPath.Count > movRange.GetPathTo(unitList[0]).Count)
            {
                GetPath(unitList[0], grid);
            }

            MoveUnit(unit.GetComponent<Unit>(), grid);


        }
    }


    private void Kappa(GameObject unit)
    {

        unitList = FindUnit();
        MovRange(unit);

        foreach (Vector3Int units in unitList)
        {
            if (currentPath.Count <= 0 || currentPath.Count > movRange.GetPathTo(units).Count)
            {
                GetPath(units, grid);
            }
        }

        unitList.Clear();
        currentPath.Clear();
    }


    private void Undead(GameObject unit)
    {
        unitList = FindUnit();

        MovRange(unit);
        foreach (Vector3Int units in unitList)
        {
            if (currentPath.Count <= 0 || currentPath.Count > movRange.GetPathTo(units).Count)
            {
                GetPath(units, grid);
            }
        }
        MoveUnit(unit.GetComponent<Unit>(), grid);
        unitList.Clear();
        currentPath.Clear();
    }

    
    IEnumerator MovEnemy()
    {
        tr.turn = false;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Ennemi"))
        {
            if (unit.GetComponent<Chara>().Classe1 == Chara.Classe.Oni)
            {
                Oni(unit, grid);
                Debug.Log("Oni");
            }
            else if (unit.GetComponent<Chara>().Classe1 == Chara.Classe.Kappa)
            {
                Kappa(unit);
                Debug.Log("Kappa");
            }
            else
            {
                Undead(unit);
                Debug.Log("Undead");
            }

            yield return new WaitForSeconds(1);
        }
        tr.turn = true;
        tr.UM.PlayersTurn = true;

    }
}
