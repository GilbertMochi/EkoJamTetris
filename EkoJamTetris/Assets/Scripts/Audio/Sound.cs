using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    [Range(0.5f, 1.5f)]
    public float pitch;
    public string name;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
