using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMood : MonoBehaviour
{
    [Header("State Bubbles")]
    [SerializeField] private GameObject _inLineBubble;
    [SerializeField] private GameObject _lookingForTapeBubble;
    
    [Header("Mood Bubbles")]
    [SerializeField] private GameObject _happyBubble;
    [SerializeField] private GameObject _mehBubble;
    [SerializeField] private GameObject _angryBubble;

    [Header("Bubble Position")]
    [SerializeField] private Transform _bubblePos;
    private int _mood = 0;
    
    // Start is called before the first frame update
    internal void incrementMood( int toAdd) => _mood += toAdd;
    internal void decrementMood( int toSub) => _mood -= toSub;

    internal GameObject CreateStateBubble(AIState state)
    {
        switch (state)
        {
            case AIState.BrowsingTapes:
                return (GameObject)Instantiate(_lookingForTapeBubble, _bubblePos.position, _bubblePos.rotation, _bubblePos);
            case AIState.WaitingInLine:
                return (GameObject)Instantiate(_inLineBubble, _bubblePos.position, _bubblePos.rotation, _bubblePos);
        }
        
        return null;
    }
    
    internal GameObject CreateMoodBubble()
    {
        if (_mood < 0)
            return (GameObject)Instantiate(_angryBubble, _bubblePos.position, _bubblePos.rotation, _bubblePos);
        if (_mood > 0)
            return (GameObject)Instantiate(_happyBubble, _bubblePos.position, _bubblePos.rotation, _bubblePos);

        return (GameObject)Instantiate(_mehBubble, _bubblePos.position, _bubblePos.rotation, _bubblePos);
    }

    private void OnDestroy()
    {
        ScoreKeeper.Singleton.ModScore(_mood);
    }
}
