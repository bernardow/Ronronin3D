using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioSource Source;
    public AudioClip clip;
    public float Volume;
    public float Pitch;
    public bool Loop;
    public bool PlayOnAwake;
}
