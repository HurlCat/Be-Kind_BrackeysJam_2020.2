using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public WaypointType type;

    [CanBeNull]
    public Shelf shelf = null;
    
    public Transform[] markers;

    private void Awake()
    {
        if ((int)type < 6)
            shelf = GetComponent<Shelf>();
    }
}

public enum WaypointType
{
    ActionShelf,
    HorrorShelf,
    ComedyShelf,
    RomanceShelf,
    DocuShelf,
    MysteryShelf,
    Register,
    Exit
}
