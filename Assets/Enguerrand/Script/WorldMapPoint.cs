using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPoint : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }

    [field: SerializeField] public bool Locked {get; private set;}

    [field : SerializeField]public GameObject panelInfo { get; private set; }

    public void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            ID = transform.parent.GetChild(i).gameObject == gameObject ? i : ID;
        }
    }
    void Start()
    {
        if (TryGetComponent<Button>(out var b))
            b.onClick.AddListener(() => NavigationController.Instance.SetTargetPoint(this));
    }

}
