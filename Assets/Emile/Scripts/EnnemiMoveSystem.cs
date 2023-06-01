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
    private List<GameObject> ennemiList = new List<GameObject>();
    private List<Vector3Int> unitList = new List<Vector3Int>();

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

    private void MovRange(GameObject tamere)
    {
       movRange= GraphSearch.BFSGetRange(grid, grid.GetClosestHex(tamere.transform.position),100);
    }

    public void GetPath(Vector3Int selectedHexPos, HexGrid grid)
    {
        movRange.GetRangePos().ToList().ForEach(x => Debug.Log(x));
        if (movRange.GetRangePos().ToList().Exists(x => x .Equals(selectedHexPos)))
        {
            currentPath = movRange.GetPathTo(selectedHexPos);
        }
    }
    public void MoveUnit(Unit selectedUnit, HexGrid grid)
    {

        var t = currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList();

        if(currentPath.Count>1)
        selectedUnit.MoveThroughPathE(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList(), selectedUnit.Mov);
    }


    private void OnNextTurn()
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Ennemi"))
        {
            unitList = FindUnit();
            MovRange(unit);
            foreach (Vector3Int units in unitList)
            {
                if (currentPath.Count<=0 || currentPath.Count > movRange.GetPathTo(units).Count)
                {
                    GetPath(units, grid);

                }
            }
            MoveUnit(unit.GetComponent<Unit>(), grid);
        }
    }
    //execute OnNexturn every 3 seconds
    IEnumerator Bob()
    {
        OnNextTurn();
        yield return new WaitForSeconds(1);       
        StartCoroutine(Bob());
    }
    private void Start()
    {
        grid = FindObjectOfType<HexGrid>();
        StartCoroutine(Bob());
    }
}
