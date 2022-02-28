using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0.0f, 1.0f)] public float volume;
    [HideInInspector] public AudioSource source;
}
