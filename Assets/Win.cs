using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] MoveDoor[] doors;

    private void Start()
    {
        foreach(MoveDoor door in doors)
        {
            door.ShutDoor();
        }
    }
}
