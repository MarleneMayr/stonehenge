using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioManager.GlobalSound name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;

    [Range(0f, 1f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
