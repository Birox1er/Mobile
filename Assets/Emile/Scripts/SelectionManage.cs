using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManage : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private List<Vector3Int> neighs;
    public LayerMask selectionMask;
    public HexGrid hexGrid;
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

    }
    public void handleTouch(Vector3 fingerPos)
    {
        GameObject target;
        if(FindTarget(fingerPos,out target))
        {
            Hex selectedHex = target.GetComponent<Hex>();
            selectedHex.DisableGlow();
            if (neighs != null)
            {
                foreach (Vector3Int neigh in neighs)
                {
                    Debug.Log("321.4");
                    hexGrid.GetTileAt(neigh).DisableGlow();
                }
            }         
            Debug.Log("321.0");
            Hex selectedTile = target.GetComponent<Hex>();
            //neighs = hexGrid.GetNeighbours(selectedTile.HexCoord);
            BFSResult bfsResult = GraphSearch.BFSGetRange(hexGrid, selectedTile.HexCoord, 3);
            neighs = new List<Vector3Int>(bfsResult.GetRangePos());
            Debug.Log($"Neighours form {selectedTile.HexCoord} are");
            foreach (Vector3Int neigh in neighs)
            {
                Debug.Log("321.5");
                hexGrid.GetTileAt(neigh).EnableGlow();
            }
        }

    }

    private bool FindTarget(Vector3 fingerPos, out GameObject target)
    {

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(fingerPos);
        if(Physics.Raycast(ray, out hit, 100, selectionMask))
        {
            target = hit.collider.gameObject;
            return true;
        }
        target = null;
        return false;
    }
}
