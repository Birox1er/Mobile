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
    public bool Valid { get => valid; set => valid = value; }

    [SerializeField] private Sprite validated;

    [SerializeField] private bool valid /*= false*/;
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
        if (valid == true)
        {
            GetComponent<Image>().sprite = validated;
        }
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

