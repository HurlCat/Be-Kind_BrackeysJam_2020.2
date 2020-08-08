using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void EnableTutorials(bool enabled)
    {
        GameController.singleton.tutorialsEnabled = enabled;
    }
}
