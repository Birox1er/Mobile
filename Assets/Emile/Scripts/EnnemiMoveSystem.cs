using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnnemiMoveSystem : MonoBehaviour
{
    private BFSResult movRange;
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    private HexGrid grid;
    private List<Vector3Int> unitList = new List<Vector3Int>();
    [SerializeField] TurnResolution tr;
    [SerializeField] Return rs;

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
            if (unit[i].GetComponent<Chara>().GetCurrentHealth() > lowerUnit.GetComponent<Chara>().GetCurrentHealth())
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

            unitList.Clear();
            currentPath.Clear();
        }
    }


    private void Kappa(GameObject unit)
    {
        unitList = FindUnit();
        MovRange(unit);
        bool isClose = false;
        Vector3Int enemyTile = Vector3Int.zero;

        Vector3Int tileSelected = Vector3Int.zero;
        List<Vector3Int> tileSelectedNeighbours2 = new List<Vector3Int>();

        foreach (Vector3Int units in unitList)
        {
            if (currentPath.Count <= 0 || currentPath.Count > movRange.GetPathTo(units).Count)
            {
                GetPath(units, grid);
                tileSelected = units;
            }
        }

        List<Vector3Int> tileSelectedNeighbours = grid.GetNeighbours(tileSelected);

        foreach (Vector3Int tile in tileSelectedNeighbours)
        {
            tileSelectedNeighbours2.AddRange(grid.GetNeighbours(tile));
        }

        foreach (Vector3Int unitTile in unitList)
        {
            foreach (Vector3Int tile in grid.GetNeighbours(grid.GetClosestHex(unit.transform.position)))
            {
                if (unitTile == tile)
                {
                    isClose = true;
                    enemyTile = tile;
                    break;
                }
            }
        }

        foreach (Vector3Int tile in tileSelectedNeighbours)
        {
            tileSelectedNeighbours2.Remove(tile);
        }

        if (isClose)
        {
            List<Vector3Int> validTiles = new List<Vector3Int>();
            foreach (Vector3Int tile in tileSelectedNeighbours2)
            {
                if (movRange.GetPathTo(tile).Count >= 3 && !unitList.Contains(tile) && !IsAdjacentToUnit(tile))
                {
                    validTiles.Add(tile);
                }
            }

            if (validTiles.Count > 0)
            {
                Vector3Int furthestTile = validTiles[0];
                foreach (Vector3Int tile in validTiles)
                {
                    if (movRange.GetPathTo(tile).Count > movRange.GetPathTo(furthestTile).Count)
                    {
                        furthestTile = tile;
                    }
                }

                GetPath(furthestTile, grid);
            }
            else
            {
                Vector3Int direction = enemyTile - tileSelected;
                Vector3Int targetTile = tileSelected - direction;

                if (!unitList.Contains(targetTile) && movRange.GetPathTo(targetTile).Count >= 3 && !IsAdjacentToUnit(targetTile))
                {
                    GetPath(targetTile, grid);
                }
                else
                {
                    // Si aucune fuite n'est possible, rester sur place
                    GetPath(tileSelected, grid);
                }
            }
        }
        else
        {
            foreach (Vector3Int tile in tileSelectedNeighbours2)
            {
                if (currentPath.Count <= 0 || currentPath.Count > movRange.GetPathTo(tile).Count && !unitList.Contains(tile) && !IsAdjacentToUnit(tile))
                {
                    GetPath(tile, grid);
                }
            }
        }

        MoveUnit(unit.GetComponent<Unit>(), grid);

        unitList.Clear();
        currentPath.Clear();
    }

    private bool IsAdjacentToUnit(Vector3Int tile)
    {
        foreach (Vector3Int unitTile in unitList)
        {
            if (grid.GetNeighbours(unitTile).Contains(tile))
            {
                return true;
            }
        }
        return false;
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
                Debug.Log(units);
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

            }
            else if (unit.GetComponent<Chara>().Classe1 == Chara.Classe.Kappa)
            {
                Kappa(unit);
            }
            else
            {
                Undead(unit);

            }

            yield return new WaitForSeconds(1);
        }
        rs.SaveTurn();

        tr.turn = true;
        tr.UM.PlayersTurn = true;
        
    }
}
