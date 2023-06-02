using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    // Start is called before the first frame update
    private HexCoord _hexCoord;
    private GlowMov glow;
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private HexType _hexType;
    public Vector3Int HexCoord => _hexCoord.GetHexCoord();

    public int GetCost() => _hexType switch
    {
        HexType.Default => 1,
        HexType.Forest => 1,
        HexType.River => 3,
        HexType.Hill => 1,
        _ => throw new Exception($"{_hexType} not supported")
    };
    public bool IsObstacle()
    {
        return this._hexType == HexType.Obstacle;
    }
    public HexType GetTypes()
    {
        return this._hexType;
    }
    private void Awake()
    {
        _hexCoord = GetComponent<HexCoord>();
        glow = GetComponent<GlowMov>();
    }
    public void EnableGlow()
    {
        glow.ToggleGlow(true);
    }
    public void DisableGlow()
    {
        glow.ToggleGlow(false);
    }

    public enum HexType
    {
        None,
        Default,
        Forest,
        River,
        Hill,
        Obstacle
    }

    internal void ResetGlow()
    {
        glow.ResetGlowHighlight();
    }

    internal void GlowPath()
    {
        glow.HighlightValidPath();
    }

    public void SetIsOccupied(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }
}
