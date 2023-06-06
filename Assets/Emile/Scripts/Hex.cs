using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Hex : MonoBehaviour
{
    // Start is called before the first frame update
    private HexCoord _hexCoord;
    private GlowMov glow;
    [SerializeField] private bool isOccupied;
    [SerializeField] private HexType _hexType;
    [SerializeField] List<GameObject> tiles;
    [SerializeField] List<ListWrapper> props=new List<ListWrapper>();
    [SerializeField] int propInt;


    public HexType hexType { get => _hexType; }
    public Vector3Int HexCoord => _hexCoord.GetHexCoord();

    public List<GameObject> Tiles { get => tiles; }
    public List<ListWrapper> Props { get => props; }
    public int PropInt { get => propInt; }

    public int GetCost() => _hexType switch
    {
        HexType.Default => 1,
        HexType.Forest => 1,
        HexType.River => 3,
        HexType.Hill => 1,
        _ => throw new Exception($"{_hexType} not supported")
    };
    private void Start()
    {

    }
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
        Debug.Log(isOccupied);
        this.isOccupied = isOccupied;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;

    }
    public void Recreate()
    {
        switch (_hexType)
        {
            case Hex.HexType.Default:
                foreach (GameObject tile in tiles)
                {
                    tile.SetActive(false);
                }
                tiles[0].SetActive(true);
                foreach (ListWrapper prop in props)
                {
                    foreach (GameObject pro in prop.Prop)
                    {
                        pro.SetActive(false);
                    }
                }
                props[0].Prop[propInt].SetActive(true);
                break;
            case Hex.HexType.River:
                foreach (GameObject tile in tiles)
                {
                    tile.SetActive(false);
                }
                tiles[2].SetActive(true);
                foreach (ListWrapper prop in props)
                {
                    foreach (GameObject pro in prop.Prop)
                    {
                        pro.SetActive(false);
                    }
                }
                props[2].Prop[propInt].SetActive(true);
                break;
            case Hex.HexType.Forest:
                foreach (GameObject tile in tiles)
                {
                    tile.SetActive(false);
                }
                tiles[1].SetActive(true);
                foreach (ListWrapper prop in props)
                {
                    foreach (GameObject pro in prop.Prop)
                    {
                        pro.SetActive(false);
                    }
                }
                props[1].Prop[propInt].SetActive(true);
                break;
            case Hex.HexType.Obstacle:
                foreach (GameObject tile in tiles)
                {
                    tile.SetActive(false);
                }
                tiles[1].SetActive(true);
                foreach (ListWrapper prop in props)
                {
                    foreach (GameObject pro in prop.Prop)
                    {
                        pro.SetActive(false);
                    }
                }
                props[3].Prop[propInt].SetActive(true);
                break;
        }
    }

    public void GetInfoTile()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            tiles.Add(child.gameObject);
        }
        
    }
    public void GetInfoProp()
    {
        Debug.Log(transform.GetChild(1).childCount);
        int i = 0;
        foreach (Transform children in transform.GetChild(1))
        {
            Debug.Log(children.childCount);
            
            props.Add(new ListWrapper());
            Debug.Log(props.Count);
            Debug.Log(props[i].Prop.Count);
            foreach (Transform child in children)
            {
                props[i].Prop.Add(child.gameObject);
            }
            i++;
        }  
    }

}
[CustomEditor(typeof(Hex))]
public class HexEdit : Editor
{
    public override void OnInspectorGUI()
    {
        var hex = (Hex)target;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            if (hex.Tiles.Count == 0)
            {
                hex.GetInfoTile();
            }
            if(hex.Props.Count == 0)
            {
                hex.GetInfoProp();
            }
            hex.Recreate();
        }
    }
}
[System.Serializable]
public class ListWrapper
{
    public List<GameObject> Prop=new List<GameObject>();
}
