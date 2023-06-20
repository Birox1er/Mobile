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

    [SerializeField] private Sprite validated;

    [SerializeField] private Sprite unvalidated;

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
        if (ID == 0)
        {
            panelInfo.SetActive(true);
        }
        if (LevelManager.Instance.IsLevelValid(ID+3))
        {
            GetComponent<Image>().sprite = validated;
        }
        else
        {
            GetComponent<Image>().sprite = unvalidated;
        }
        if (LevelManager.Instance.IsLevelUnlocked(ID+2))
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
        //panelInfo.SetActive(true);
        GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void Lock()
    {
        Locked = true;
        //panelInfo.SetActive(true);
        GetComponent<Image>().color = new Color(140, 140, 140);
    }
}

