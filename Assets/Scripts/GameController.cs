using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController singleton;
    
    public int rewindProbability = 33; // probability that tapes need to be rewound

    public int maxCustomersInStore = 5;

    public int stockPerGenre = 1;

    private void Awake()
    {
        if (singleton != null)
            Destroy(this);
        else
            singleton = this;
    }
}