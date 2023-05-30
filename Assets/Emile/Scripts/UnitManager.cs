using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private HexGrid grid;
    [SerializeField] private MovSystem movSystem;
    public bool PlayersTurn { get; private set; } = true;

    private Unit selectedUnit;
    private Hex previouslySelectedHex;

    public void HandleUnitSelected(GameObject unit)
    {
        if (PlayersTurn == false)
        {
            return;
        }
        Unit unitRef = unit.GetComponent<Unit>();
        if (CheckIfTheSameUnitSelected(unitRef))
        {
            return;
        }
        PrepareUnitForMov(unitRef);
    }

    private void PrepareUnitForMov(Unit unitRef)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection();
        }
        this.selectedUnit = unitRef;
        this.selectedUnit.Select();
        movSystem.ShowRange(this.selectedUnit, this.grid);
    }

    private bool CheckIfTheSameUnitSelected(Unit unitRef)
    {
        if (this.selectedUnit == unitRef)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }
    public void HandleTerrainSelected(GameObject hex)
    {
        if(selectedUnit==null||PlayersTurn==false)
        {
            return;
        }
        Hex selectedHex = hex.GetComponent<Hex>();
        if (HandleHexOutOfRange(selectedHex.HexCoord) || HandleSelectedHexIsUnitHex(selectedHex.HexCoord))
        {
            return;
        }
        HandleTargetHexSelected(selectedHex);
    }

    private bool HandleHexOutOfRange(Vector3Int hexCoord)
    {
        if(movSystem.IsHexInRange(hexCoord) == false){
            return true;
        }
        return false;
    }

    private bool HandleSelectedHexIsUnitHex(Vector3Int hexCoord)
    {
        if (hexCoord == grid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    private void HandleTargetHexSelected(Hex selectedHex)
    {
        if(previouslySelectedHex==null ||previouslySelectedHex !=selectedHex)
        {
            previouslySelectedHex = selectedHex;
            movSystem.ShowPath(selectedHex.HexCoord, this.grid);
        }
        else
        {
            movSystem.MoveUnit(selectedUnit,this.grid);
            //PlayersTurn = false;
            //selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
    }

    /*private void ResetTurn(Unit obj)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        Debug.Log("Ahhh");
        PlayersTurn = true;
    }*/

    private void ClearOldSelection()
    {
        previouslySelectedHex = null;
        this.selectedUnit.Deselect();
        movSystem.HideRange(this.grid);
        this.selectedUnit = null;

    }
}
    
