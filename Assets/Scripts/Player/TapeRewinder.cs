using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

public class TapeRewinder : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private GameObject _bubble;
    
    private VHSTape _currentTape;
    private bool _rewinding = false;
    private bool _tapeInside = false;

    [SerializeField] private float _rewindTime = 10f;
    private float _rewindTimer;

    [SerializeField] private AudioClip _insertTape, _doneRewinding, _takeTape;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_rewinding) // if we aren't rewinding
            return;

        _rewindTimer += Time.deltaTime;
        if (_rewindTimer > _rewindTime) // when done rewinding
        {
            _rewindTimer = 0f;

            _rewinding = false; // allow tape to be grabbed
            _animator.Play("DoneRewinding");
            _bubble.SetActive(true);
            Util.PlayAudio(_audioSource, _doneRewinding);
        }
    }

    public void InsertTape(VHSTape tape)
    {
        tape.rewound = true;
        _currentTape = tape;
        
        _rewinding = true;
        _tapeInside = true;

        _bubble.SetActive(false);
        Util.PlayAudio(_audioSource, _insertTape);
        _animator.Play("Rewinding");
    }

    public VHSTape GiveTapeToPlayer()
    {
        _tapeInside = false;
        _rewinding = false;
        
        Util.PlayAudio(_audioSource, _takeTape);
        return _currentTape;
    }

    public bool IsRewinding() => _rewinding;
    public bool IsFull() => _tapeInside;
}
