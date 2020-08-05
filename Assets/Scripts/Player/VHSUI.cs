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
    public Sprite[] goodStamps;
    public Sprite[] genreSprites;

    private void Update() // gotta change this later to be more performant
    {
        artwork.sprite = tape.graphics;
        
        timeStamp.sprite = tape.rewound ? goodStamps[UnityEngine.Random.Range(0, goodStamps.Length)] : rewindStamps[UnityEngine.Random.Range(0, rewindStamps.Length)];
        
        genre.sprite = genreSprites[(int)tape.genre];
    }
}
