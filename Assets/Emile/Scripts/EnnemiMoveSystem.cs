using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnnemiMoveSystem : MonoBehaviour
{
    private BFSResult movRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    [SerializeField] private HexGrid grid;


    //Search all tag "Unit" with the pathfinding
    public void SearchUnit(HexGrid grid)
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Calculaterange(unit.GetComponent<Unit>(), grid);

        }
    }

    private void Calculaterange(Unit selectedUnit, HexGrid grid)
    {
        movRange = GraphSearch.BFSGetRange(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.Mov);
    }

    public void MoveEnnemi(Unit selectedUnit, HexGrid grid)
    {
        selectedUnit.MoveThroughPath(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList());
    }

    public bool IsHexInRange(Vector3Int hexPos)
    {
        return movRange.IsHexPosInRange(hexPos);
    }

    //Select the path to the nearest unit
    public void SelectPath(Vector3Int selectedHexPos, HexGrid grid)
    {
        if (movRange.GetRangePos().Contains(selectedHexPos))
        {

            currentPath = movRange.GetPathTo(selectedHexPos);
            foreach (Vector3Int hexPos in currentPath)
            {
                grid.GetTileAt(hexPos).GlowPath();
            }
        }
    }

    //make the ennei move to the nearest each frame
    public void MoveEnnemiToNearest(Unit selectedUnit, HexGrid grid)
    {
        Debug.Log("Update");
        if (currentPath.Count > 0)
        {
            Debug.Log("Update 2");
            selectedUnit.MoveThroughPath(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList());
        }
    }

    /*private void Update()
    {
        
        if (grid == null)
        {
            grid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        }
        SearchUnit(grid);
        MoveEnnemiToNearest(gameObject.GetComponent<Unit>(), grid);
    }*/
    IEnumerator Move()
    {
        yield return new WaitForSeconds(3);
        if (grid == null)
        {
            grid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        }
        SearchUnit(grid);
        MoveEnnemiToNearest(gameObject.GetComponent<Unit>(), grid);
    }




}
