using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public Dictionary<Vector3Int, Hex> hexTileD = new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursD = new Dictionary<Vector3Int, List<Vector3Int>>();
    private void Start()
    {
        hexTileD.Clear();
        hexTileNeighboursD.Clear();
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTileD[hex.HexCoord] = hex;
        }
        EnnemiMoveSystem a = FindObjectOfType<EnnemiMoveSystem>();
        Debug.Log(hexTileD.Count);
        if(a != null )
        a.FirstTurn();
    }
    public Hex GetTileAt(Vector3Int hexCoordinate)
    {
        Hex result = null;
        hexTileD.TryGetValue(hexCoordinate, out result);
        return result;
    }
    public Hex GetTileAtClosestHex(Vector3 worldPos)
    {
        Hex result = null;
        hexTileD.TryGetValue(HexCoord.ConvertPositionToOffset(worldPos), out result);
        return result;
    }
    public List<Vector3Int> GetNeighbours(Vector3Int tileCoord)
    {
        if (hexTileD.ContainsKey(tileCoord) == false)
        {
            return new List<Vector3Int>();
        }
        if (hexTileNeighboursD.ContainsKey(tileCoord))
        {
            return hexTileNeighboursD[tileCoord];
        }
        hexTileNeighboursD.Add(tileCoord, new List<Vector3Int>());
        foreach(var direction in Direction.GetDirList(tileCoord.x))
        {
            if (hexTileD.ContainsKey(tileCoord + direction))
            {
                hexTileNeighboursD[tileCoord].Add(tileCoord + direction);
            }
        }
        return hexTileNeighboursD[tileCoord];
    }
    internal Vector3Int GetClosestHex(Vector3 worldPos)
    {
        worldPos.z = 0;
        return HexCoord.ConvertPositionToOffset(worldPos);
    }
}
public static class Direction
{
    public static List<Vector3Int> evenDirectionOffset = new List<Vector3Int>
    {
        new Vector3Int(0,1,0),//N
        new Vector3Int(-1,0,0),//NW
        new Vector3Int(-1,-1,0),//SW
        new Vector3Int(0,-1,0),//S
        new Vector3Int(1,-1,0),//SE
        new Vector3Int(1,0,0)//NW
    };
    public static List<Vector3Int> oddDirectionOffset = new List<Vector3Int>
    {
        new Vector3Int(0,1,0),//N
        new Vector3Int(-1,1,0),//NW
        new Vector3Int(-1,0,0),//SW
        new Vector3Int(0,-1,0),//S
        new Vector3Int(1,0,0),//SE
        new Vector3Int(1,1,0)//NW
    };
    public static List<Vector3Int> GetDirList(int x) => x % 2 == 0 ? evenDirectionOffset : oddDirectionOffset;
}
