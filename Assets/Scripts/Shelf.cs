using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private int _tapesInStock;
    [SerializeField] private ShelfAnimator _shelfAnimator;

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
        if(_shelfAnimator != null)
            _shelfAnimator.UpdateSprite(GetCapacityInPercent());
    }

    private void Update()
    {
        if (GetCapacityInPercent() < .33f)
        {
            TutorialEvents.singleton.FirstLowStock();
        }
    }

    public void IncrementStock() 
    {
        _tapesInStock++;
        if(_shelfAnimator != null)
            _shelfAnimator.UpdateSprite(GetCapacityInPercent());
    }
    public bool CompareGenres(Genre genre) => (genre == _genre) ? true : false; // returns true if param == this.genre
    public bool IsFull() => _tapesInStock >= _capacity; // returns true if at capacity
    public float GetCapacityInPercent() => ((float)_tapesInStock / (float)_capacity);
    
    public bool takeFromShelf(int quantity)
    {
        if(_shelfAnimator != null)
            _shelfAnimator.UpdateSprite(GetCapacityInPercent());
        if (_tapesInStock - quantity >= 0)
        {
            _tapesInStock -= quantity;
            return true;
        }
        else
        {
            return false;
        }
    }
}