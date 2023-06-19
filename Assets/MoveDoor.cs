using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    bool shutDoor = false;
    bool openDoor = false;
    [SerializeField]bool right;
    [SerializeField] float distance;

    private void Update()
    {
        if(shutDoor)
        {
            Debug.Log(right);
            float posx = GetComponent<RectTransform>().anchoredPosition.x;
            if(!right)
            {
                if (posx < -distance*0.05f)
                {
                    posx += distance*2.5f * Time.deltaTime;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, GetComponent<RectTransform>().anchoredPosition.y);
                }
                else
                {
                    shutDoor = false;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(0, GetComponent<RectTransform>().anchoredPosition.y);
                }
                
            }
            else
            {
                if (posx > distance * 0.05f)
                {
                    posx -= distance*2.5f * Time.deltaTime;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, GetComponent<RectTransform>().anchoredPosition.y);
                }
                else
                {
                    shutDoor = false;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(0, GetComponent<RectTransform>().anchoredPosition.y);
                }
                
            }
            
        }
        if (openDoor)
        {
            float posx = GetComponent<RectTransform>().anchoredPosition.x;
            if (!right)
            {
                if (posx>-distance+ distance * 0.05f)
                {
                    posx -= distance * 2.5f * Time.deltaTime;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, GetComponent<RectTransform>().anchoredPosition.y);
                }
                else
                {
                    openDoor = false;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(-distance, GetComponent<RectTransform>().anchoredPosition.y);
                }
                
            }
            else
            {
                if (posx <distance- distance * 0.05f)
                {
                    posx += distance*2.5f * Time.deltaTime;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, GetComponent<RectTransform>().anchoredPosition.y);
                }
                else
                {
                    openDoor = false;
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(distance, GetComponent<RectTransform>().anchoredPosition.y);
                }
                
            }
        }
    }
    // Start is called before the first frame update
    public void ShutDoor()
    {
        shutDoor = true;
        Debug.Log(shutDoor);
    }
    public void OpenDoor()
    {
        openDoor = true;
    }
}
