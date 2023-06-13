using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovSystem : MonoBehaviour
{
    private BFSResult movRange = new BFSResult();
    private BFSResult AtkRangeMax = new BFSResult();
    private BFSResult AtkRangeMin = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    public void HideRange(HexGrid hexGrid)
    {
        Debug.Log(movRange.visitedNodeD.Count);
        Debug.Log(AtkRangeMax.visitedNodeD);
        if (movRange.visitedNodeD.Count >0)
        {
            foreach (Vector3Int hexPos in movRange.GetRangePos())
            {
                hexGrid.GetTileAt(hexPos).DisableGlow();
            }
        }
        if (AtkRangeMax.visitedNodeD != null)
        {
            if (AtkRangeMax.visitedNodeD.Count > 0)
            {
                foreach (Vector3Int hexPos in AtkRangeMax.GetRangePos())
                {
                    hexGrid.GetTileAt(hexPos).DisableGlowA();
                }
            }
        }
        
        movRange = new BFSResult();
        AtkRangeMax = new BFSResult();
        AtkRangeMin = new BFSResult();
    }
    public void HideRangeA(HexGrid hexGrid)
    {
        foreach (Vector3Int hexPos in movRange.GetRangePos())
        {
            hexGrid.GetTileAt(hexPos).DisableGlowA();
        }
        
}
    public void ShowRange(Unit selectedUnit,HexGrid grid)
    {
        Calculaterange(selectedUnit, grid);
        CalculaterangeAtk(selectedUnit, grid);
        if(selectedUnit.GetComponent<Chara>().Allied == true)
        {
            foreach (Vector3Int hexPos in movRange.GetRangePos())
            {
                grid.GetTileAt(hexPos).EnableGlow();
            }
        }
    }
    public void ShowRangeAtk(Unit selectedUnit, HexGrid grid)
    {
        CalculaterangeAtk(selectedUnit, grid);
        Calculaterange(selectedUnit, grid);
        AtkRangeMax.GetRangePos().ToList().ForEach(x => Debug.Log("AH" +x));
        AtkRangeMin.GetRangePos().ToList().ForEach(x => Debug.Log("3H" + x));
        foreach (Vector3Int hexPos in AtkRangeMax.GetRangePos())
        {
            if (!AtkRangeMin.GetRangePos().Contains(hexPos))
            {
                grid.GetTileAt(hexPos).EnableGlowA(selectedUnit);
            }
        }
    }
    private void Calculaterange(Unit selectedUnit, HexGrid grid)
    {
        movRange = GraphSearch.BFSGetRange(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.Mov);
    }
    private void CalculaterangeAtk(Unit selectedUnit, HexGrid grid)
    {
        if(selectedUnit.GetComponent<Chara>().Classe1==Chara.Classe.Archer|| selectedUnit.GetComponent<Chara>().Classe1 == Chara.Classe.Kappa)
        {
            AtkRangeMax = GraphSearch.BFSGetAttackRanged(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.GetComponent<Chara>().RangeMax);
            AtkRangeMin = GraphSearch.BFSGetAttackRanged(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.GetComponent<Chara>().RangeMin - 1);
        }
        else
        {
            AtkRangeMax = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.GetComponent<Chara>().RangeMax);
            AtkRangeMin = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.GetComponent<Chara>().RangeMin - 1);
        }
    }
    public void ShowPath(Vector3Int selectedHexPos, HexGrid grid)
    {
        if (movRange.GetRangePos().Contains(selectedHexPos))
        {
            foreach (Vector3Int hexPos in currentPath)
            {
                grid.GetTileAt(hexPos).ResetGlow();
            }
            currentPath = movRange.GetPathTo(selectedHexPos);
            foreach (Vector3Int hexPos in currentPath)
            {
                grid.GetTileAt(hexPos).GlowPath();
            }
        }
    }
    public void MoveUnit(Unit selectedUnit, HexGrid grid)
    {
        if (selectedUnit.GetComponent<Chara>().Allied == true)
        {
            selectedUnit.MoveThroughPath(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList());
            selectedUnit.SetHasMoved(true);
        }
    }
    public bool IsHexInRange(Vector3Int hexPos)
    {
        return movRange.IsHexPosInRange(hexPos);
    }
}
