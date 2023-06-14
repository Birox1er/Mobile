using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private HexGrid grid;
    [SerializeField] private MovSystem movSystem;
    public bool PlayersTurn { get;  set; } = false;

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
        else
        {
            if (unitRef.GetComponent<Chara>().Allied)
            {
                PrepareUnitForMov(unitRef);
            }
            else
            {
                PrepareUnitForMovE(unitRef);
            }
        }
    }
    private void PrepareUnitForMovE(Unit unitRef)
    {
        Debug.Log(unitRef.HasMoved());
        if (this.selectedUnit != null)
        {
            ClearOldSelection(unitRef);
        }
        this.selectedUnit = unitRef;
        Debug.Log("1");
        movSystem.ShowRangeAtk(this.selectedUnit, this.grid);
    }
    private void PrepareUnitForMov(Unit unitRef)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection(unitRef);
        }
        this.selectedUnit = unitRef;
        if (!unitRef.HasMoved())
        {
            movSystem.ShowRange(this.selectedUnit, this.grid);
        }
        else
        {
            movSystem.ShowRangeAtk(this.selectedUnit, this.grid);
        }
    }

    private bool CheckIfTheSameUnitSelected(Unit unitRef)
    {
        if (this.selectedUnit == unitRef)
        {
            ClearOldSelection(unitRef);
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
            selectedUnit.Deselect();
            ClearOldSelection(selectedUnit);
        }
        return false;
    }

    private bool HandleSelectedHexIsUnitHex(Vector3Int hexCoord)
    {
        if (hexCoord == grid.GetClosestHex(selectedUnit.transform.position))
        {
            ClearOldSelection(selectedUnit);
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
            ClearOldSelection(selectedUnit);
        }
    }

    /*private void ResetTurn(Unit obj)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        Debug.Log("Ahhh");
        PlayersTurn = true;
    }*/

    private void ClearOldSelection(Unit unit)
    {
        previouslySelectedHex = null;
        movSystem.HideRange(this.grid);
        this.selectedUnit = null;

    }
}
    
