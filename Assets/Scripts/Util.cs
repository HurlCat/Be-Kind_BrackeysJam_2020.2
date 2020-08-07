using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    static public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
