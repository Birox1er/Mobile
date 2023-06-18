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
    private int i=-1; 

    public void HandleUnitSelected(GameObject unit)
    {
        if (PlayersTurn == false)
        {
            return;
        }
        Unit unitRef = unit.GetComponent<Unit>();
        if (!CheckIfTheSameUnitSelected(unitRef))
        {
            i=-1;
        }
        /*else
        {*/
            if (unitRef.GetComponent<Chara>().Allied)
            {
                PrepareUnitForMov(unitRef);
            }
            else
            {
                PrepareUnitForMovE(unitRef);
            }
        //}
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
        if(i == 1)
        {
            i = -1;
        }
        else
        {
            i = 1;
            movSystem.ShowRangeAtk(this.selectedUnit, this.grid);
        }
    }
    private void PrepareUnitForMov(Unit unitRef)
    {
        if (this.selectedUnit!=null)
        {
            ClearOldSelection(unitRef);
        }
        this.selectedUnit = unitRef;
        if (!unitRef.HasMoved())
        {
            if (i == 0)
            {
                Debug.Log(i);
                i = 1;
                movSystem.ShowRangeAtk(this.selectedUnit, this.grid);
            }
            else if (i==1)
            {
                ClearOldSelection(unitRef);
                i = -1;
            }
            else
            {
                Debug.Log("ii" + i);
                i = 0;
                movSystem.ShowRange(this.selectedUnit, this.grid);
            }
        }
        else 
        {
            i = 1;
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
        Debug.Log(HandleHexOutOfRange(selectedHex.HexCoord));
        if (HandleHexOutOfRange(selectedHex.HexCoord) || HandleSelectedHexIsUnitHex(selectedHex.HexCoord))
        {
            return;
        }
        HandleTargetHexSelected(selectedHex);
    }

    private bool HandleHexOutOfRange(Vector3Int hexCoord)
    {
        if (i == 0)
        {
            if (movSystem.IsHexInRange(hexCoord) == false)
            {
                ClearOldSelection(selectedUnit);
            }
        }
        else
        {
            if (movSystem.IsHexInRangeAtk(hexCoord) == false)
            {
                Debug.Log(selectedUnit.name);
                ClearOldSelection(selectedUnit);
            }
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
    
