using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] public string name { get; private set; }
    [SerializeField] public AudioClip clip { get; private set; }
}
