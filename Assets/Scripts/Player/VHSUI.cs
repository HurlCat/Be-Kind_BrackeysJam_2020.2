using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VHSUI : MonoBehaviour
{
    public VHSTape tape;

    public Image artwork;

    private void Update() // gotta change this later to be more performant
    {
        artwork.sprite = tape.graphics;
    }
}
