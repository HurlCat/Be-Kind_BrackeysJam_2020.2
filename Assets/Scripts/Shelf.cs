using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int _capacity;
    private int _tapesInStock;

    private Waypoint _waypoint;
    
    [SerializeField] private Genre _genre;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
        this.name = _genre + " Shelf";
    }

    private void Start()
    {
        _waypoint.type = (WaypointType)_genre;
    }

    public void IncrementStock() => _tapesInStock++;
    public bool CompareGenres(Genre genre) => (genre == _genre) ? true : false; // returns true if param == this.genre
    public bool IsFull() => _tapesInStock >= _capacity; // returns true if at capacity
}