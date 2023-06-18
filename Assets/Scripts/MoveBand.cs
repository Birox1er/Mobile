using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBand : MonoBehaviour
{
    bool fall = false;
    bool up = false;
    float vert;

    private void Start()
    {
        vert= GetComponent<RectTransform>().anchoredPosition.y;
    }
    // Update is called once per frame
    void Update()
    {
        float posy = vert;
        if (fall)
        {
            if(posy > -vert+ vert* 0.1f)
                {
                posy -= vert * 8f * Time.deltaTime;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, posy);
            }
                else
            {
                fall = false;
                GetComponent<RectTransform>().anchoredPosition = new Vector2( GetComponent<RectTransform>().anchoredPosition.x, -vert);
            }
        }
        if(up)
        {
            if (posy < vert - vert * 0.1f)
            {
                posy += vert * 8f * Time.deltaTime;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, posy);
            }
            else
            {
                up = false;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, vert);
            }
        }
    }
    public void Fall()
    {
        StartCoroutine(setFall());
    }
    IEnumerator setFall()
    {
        yield return new WaitForSeconds(0.4f);
        fall = true;
    }
    public void Up()
    {
        up = true;
    }
}
