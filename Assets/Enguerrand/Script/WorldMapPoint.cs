using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPoint : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }

    [field: SerializeField] public bool Locked { get; private set; }

    [field: SerializeField] public GameObject panelInfo { get; private set; }

    private Button button;

    private void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            ID = transform.parent.GetChild(i).gameObject == gameObject ? i : ID;
        }

        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void Start()
    {
        if (LevelManager.Instance.IsLevelUnlocked(ID))
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    private void OnClick()
    {
        if (!Locked)
        {
            NavigationController.Instance.SetTargetPoint(this);
        }
    }

    public void Unlock()
    {
        Locked = false;
        panelInfo.SetActive(false);
    }

    public void Lock()
    {
        Locked = true;
        panelInfo.SetActive(true);
    }
}

