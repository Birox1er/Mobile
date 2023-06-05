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

    private void MovRange(GameObject units)
    {
       movRange= GraphSearch.BFSGetRange(grid, grid.GetClosestHex(units.transform.position),100);
    }

    public void GetPath(Vector3Int selectedHexPos, HexGrid grid)
    {
        //movRange.GetRangePos().ToList().ForEach(x => Debug.Log(x));
        Debug.Log(selectedHexPos);
        Debug.Log(movRange.GetRangePos().ToList().Exists(x => x.Equals(selectedHexPos)));
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
            Debug.Log("AH2");
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
    IEnumerator MovEnemy()
    {
        Debug.Log("&hh");
        tr.turn = false;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Ennemi"))
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

            yield return new WaitForSeconds(1);
        }
        tr.turn = true;
        tr.UM.PlayersTurn = true;

    }
}
