using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoord : MonoBehaviour
{
    public static float xOff = 1.73f, yOff = 2, zOff = 1;

    [Header("Coordonée offset")]
    [SerializeField] private Vector3Int offsetCoord;

    internal Vector3Int GetHexCoord()
        => offsetCoord;
    

    private void Awake()
    {
        offsetCoord = ConvertPositionToOffset(transform.position);
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / xOff);
        int y = Mathf.CeilToInt(position.y / yOff);
        int z = Mathf.RoundToInt(position.z / zOff);
        return new Vector3Int(x, y, z);
    }
}
