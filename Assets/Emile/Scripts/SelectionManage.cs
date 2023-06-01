using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManage : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public LayerMask selectionMask;
    public UnityEvent<GameObject> OnUnitSelected;
    public UnityEvent<GameObject> TerrainSelected;
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
            if (UnitSelected(target))
            {
                Debug.Log("Z");
                OnUnitSelected?.Invoke(target);
            }
            else
            {
                TerrainSelected?.Invoke(target);
            }
        }

    }
    private bool UnitSelected(GameObject target)
    {
        return target.GetComponent<Unit>() != null;
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
