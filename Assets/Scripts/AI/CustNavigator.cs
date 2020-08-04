﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustNavigator : MonoBehaviour
{
    internal Seeker _seeker;
    private Rigidbody2D _rb;
    
    [SerializeField] internal Transform _destination;

    [SerializeField] private float _walkSpeed = 4f;

    [Tooltip("Distance customer needs to be before going to next waypoint")] 
    [SerializeField] private float _nextWaypointDistance = 3f;

    [Tooltip("Rate at which path updates")]
    [SerializeField] private float _pathUpdateRate = .5f;

    private Path _path;
    private int _currentWaypoint = 0;
    internal bool _reachedEndOfPath = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        _walkSpeed = UnityEngine.Random.Range(1.5f, 3f);
    }

    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, _pathUpdateRate);
    }

    void UpdatePath()
    {
        if(_seeker.IsDone())
            _seeker.StartPath(_rb.position, _destination.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        
        _path = p;
        _currentWaypoint = 0;

    }
    void FixedUpdate()
    {
        if (_path == null) return;

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector3 direction = ((Vector3) _path.vectorPath[_currentWaypoint] - this.transform.position).normalized;
        _rb.MovePosition(this.transform.position + direction * (Time.deltaTime * _walkSpeed));

        float distance = Vector2.Distance(this.transform.position, _path.vectorPath[_currentWaypoint]);

        if (distance < _nextWaypointDistance)
            _currentWaypoint++;

    }
}