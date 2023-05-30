using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] Scene sceneToLoad;

    public void Open()
    {
        sceneToLoad.Open();
    }
}
