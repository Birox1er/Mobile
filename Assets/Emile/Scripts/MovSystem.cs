using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovSystem : MonoBehaviour
{
    private BFSResult movRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    public void HideRange(HexGrid hexGrid)
    {
        foreach(Vector3Int hexPos in movRange.GetRangePos())
        {
            hexGrid.GetTileAt(hexPos).DisableGlow();
        }
        movRange = new BFSResult();
    }

    public void ShowRange(Unit selectedUnit,HexGrid grid)
    {
        Calculaterange(selectedUnit, grid);
        foreach (Vector3Int hexPos in movRange.GetRangePos())
        {
            grid.GetTileAt(hexPos).EnableGlow();
        }
    }

    private void Calculaterange(Unit selectedUnit, HexGrid grid)
    {
        movRange = GraphSearch.BFSGetRange(grid, grid.GetClosestHex(selectedUnit.transform.position), selectedUnit.Mov);
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
        selectedUnit.MoveThroughPath(currentPath.Select(pos => grid.GetTileAt(pos).transform.position).ToList());
    }
    public bool IsHexInRange(Vector3Int hexPos)
    {
        return movRange.IsHexPosInRange(hexPos);
    }
}
