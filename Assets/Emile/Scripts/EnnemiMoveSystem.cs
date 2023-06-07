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

            unitList.Clear();
            currentPath.Clear();
        }
    }


    private void Kappa(GameObject unit)
    {
        unitList = FindUnit();
        MovRange(unit);

        Vector3Int tileSelected = unitList.FirstOrDefault(); // Sélectionne la première unité de la liste
        GetPath(tileSelected, grid); // Définit le chemin vers cette unité comme chemin actuel

        bool isClose = unitList.Any(tile => grid.GetNeighbours(grid.GetClosestHex(unit.transform.position)).Contains(tile));

        List<Vector3Int> tileSelectedNeibourgh2 = new List<Vector3Int>();
        foreach (Vector3Int tile in grid.GetNeighbours(tileSelected))
        {
            tileSelectedNeibourgh2.AddRange(grid.GetNeighbours(tile));
        }

        List<Vector3Int> validTiles = new List<Vector3Int>();
        foreach (Vector3Int tile in tileSelectedNeibourgh2)
        {
            bool isInRange = false;
            foreach (Vector3Int unitTile in unitList)
            {
                int distance = HexDistance(tile, unitTile);
                if (distance >= 2 && distance <= 3)
                {
                    isInRange = true;
                    break;
                }
            }
            if (isInRange && HexDistance(tile, grid.GetClosestHex(unit.transform.position)) >= 2)
            {
                validTiles.Add(tile);
            }
        }

        Vector3Int furthestTile = isClose ? tileSelected : validTiles.OrderByDescending(tile => movRange.GetPathTo(tile).Count).FirstOrDefault();

        GetPath(furthestTile, grid); // Définit le chemin vers la tuile la plus éloignée

        MoveUnit(unit.GetComponent<Unit>(), grid);

        unitList.Clear();
        currentPath.Clear();
    }

    private int HexDistance(Vector3Int a, Vector3Int b)
    {
        int dX = Mathf.Abs(a.x - b.x);
        int dY = Mathf.Abs(a.y - b.y);
        int dZ = Mathf.Abs(a.z - b.z);
        return Mathf.Max(dX, dY, dZ);
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

            }
            else if (unit.GetComponent<Chara>().Classe1 == Chara.Classe.Kappa)
            {
                Kappa(unit);
                Debug.Log("Kappa");
            }
            else
            {
                Undead(unit);

            }

            yield return new WaitForSeconds(1);
        }
        tr.turn = true;
        tr.UM.PlayersTurn = true;

    }
}
