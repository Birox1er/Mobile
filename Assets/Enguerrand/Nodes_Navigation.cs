using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nodes_Navigation : MonoBehaviour
{

    private GameObject player;
    [Header("Nodes")]
    [SerializeField] private World_Map[] nodes;
    [SerializeField] private World_Map currentNode;
    [SerializeField] private World_Map target;
    private int i;
    public bool _Duration = true;


    void Awake()
    {
        currentNode = nodes[0];
        i = 0;
    }
    
    /*public void RightBtn()
    {
        if (i != nodes.Length - 1 && !nodes[i+1]._isLocked && _Duration ==true) 
        {
            currentNode.panelInfoDestination.SetActive(false);  
            _Duration = false;
            currentNode.rightBtn();
            i++;
            currentNode = nodes[i];
        }
    }
    public void LeftBtn()
    {
        if(i!=0 && _Duration == true) 
        {
            _Duration=false;
            currentNode.leftBtn();
            i--;
            currentNode = nodes[i];
        }
        
    }*/

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("level_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("level_3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("level_4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("level_5");
    }


    IEnumerator GotToDestination()
    {
        while (currentNode.transform.position != target.transform.position)
        {
            for(int i = 1; i < nodes.Length; i++)
            {

            }
            yield return null;
        }
    }
}
