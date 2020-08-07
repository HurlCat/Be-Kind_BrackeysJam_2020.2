using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VHSUI : MonoBehaviour
{
    public VHSTape tape;

    public Image artwork;
    public Image timeStamp;
    public Image genre;

    public Sprite[] rewindStamps;
    public Sprite goodStamp;
    public Sprite[] genreSprites;

    private void Start() // gotta change this later to be more performant
    {
        artwork.sprite = tape.graphics;
        
        timeStamp.sprite = tape.rewound ? goodStamp : rewindStamps[UnityEngine.Random.Range(0, rewindStamps.Length)];
        
        genre.sprite = genreSprites[(int)tape.genre];
    }
}
