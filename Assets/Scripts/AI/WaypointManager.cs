using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager singleton;
    
    public Waypoint exit;
    public Waypoint register;
    
    public List<Waypoint> shelves;

    private void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;
    }

    private void Start()
    {
        Waypoint[] worldWaypoints = FindObjectsOfType<Waypoint>();

        foreach (var waypoint in worldWaypoints)
        {
            switch (waypoint.type)
            {
                case WaypointType.Exit:
                    exit = waypoint;
                    break;
                case WaypointType.Register:
                    register = waypoint;
                    break;
                default:
                    shelves.Add(waypoint);
                    break;
            }
        }
    }

    public Transform GetRegisterWaypoint() => register.markers[UnityEngine.Random.Range(0, register.markers.Length)];

    public Transform GetExitWaypoint() => exit.markers[0];
}

