using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBell : MonoBehaviour
{
    [SerializeField] private AudioClip _doorBell;
    private AudioSource _audioSource;

    private float _coolDownInSeconds = 3f;
    private float _lastTimePlayed = 0f;
    private bool _tutorialRan = false;
    

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Customer") && Time.time > _lastTimePlayed + _coolDownInSeconds)
        {
            Util.PlayAudio(_audioSource, _doorBell);
            _lastTimePlayed = Time.time;

            if (!_tutorialRan)
            {
                TutorialEvents.singleton.FirstCustomer(other.GetComponent<CustomerMood>());
                _tutorialRan = true;
            }

        }
    }
}
