using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uex : MonoBehaviour
{
    [SerializeField] List<Sprite> ex;
    [SerializeField]MoveBand palne;
    [SerializeField] GameObject cardHlder;
    private int i;
    private void Start()
    {
        i = 0;
        transform.GetChild(0).GetComponent<Image>().sprite = ex[0];
    }
    public void S()
    {
        if (i != ex.Count - 1)
        {
            i++;
            transform.GetChild(0).GetComponent<Image>().sprite = ex[i];
        }
        else
        {
            palne.Up();
            cardHlder.SetActive(true);
        }
    }
}
