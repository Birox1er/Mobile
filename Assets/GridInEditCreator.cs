using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GridInEditCreator : MonoBehaviour
{
    [SerializeField] GameObject hex;
    [SerializeField] int length;
    [SerializeField] int width;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    // Start is called before the first frame update
    public void Recreate()
    {
        
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i % 2 == 0)
                {
                    Instantiate(hex, new Vector3(i * offsetX, j * offsetY + 1, 0), hex.transform.rotation, transform);
                }
                else
                {
                    Instantiate(hex, new Vector3(i * offsetX, j * offsetY, 0), hex.transform.rotation, transform);
                }
            }
        }
    }
}
[CustomEditor(typeof(GridInEditCreator))]
public class GridEdit : Editor
{
    public override void OnInspectorGUI()
    {
        var grid = (GridInEditCreator)target;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            grid.Recreate();
        }
    }
}
