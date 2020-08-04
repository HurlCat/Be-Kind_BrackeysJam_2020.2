using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    private CustNavigator _nav;
    private Shelf _shelf;
    
    [SerializeField] private Genre _tapeToBuy;
    [SerializeField] private bool _hasTape = false;
    
    [SerializeField] private AIState _currentState = AIState.Walking;

    [SerializeField] private float _stateDelay = .5f; // time in seconds between 

    [SerializeField] private float _patienceTime = 3f;
    private float _timer = 0;
    [SerializeField] private float _browsingTime = 5f;
    
    private float _lastStateChange = 0f;
    private float _stateTimer = 0f;

    private void Awake()
    {
        _nav = GetComponent<CustNavigator>();
    }

    private void Start()
    {
        pickGenreToBuy();
        FindWaypoint();
    }

    void Update()
    {
        Think();
    }

    void Think()
    {
        if (Time.time > _lastStateChange + _stateDelay) // if we're able to change states again
        {
            switch (_currentState) // we're gonna try and see if we can change our state
            {
                case AIState.Walking:
                    if (_nav._reachedEndOfPath) // if we get to our destination
                    {
                        _nav._destination = this.transform;
                        
                        if (_hasTape) // we came from getting our tapes
                            ChangeState(AIState.WaitingInLine);
                        else if (!_hasTape) // we haven't gotten a tape yet
                            ChangeState(AIState.BrowsingTapes);
                    }
                    break;
                case AIState.BrowsingTapes:
                    if (_timer > _browsingTime) // if we get tired of browsing
                    {
                        _timer = 0f;
                        
                        if (_hasTape) // if we got our tape
                            _nav._destination = WaypointManager.singleton.GetRegisterWaypoint();
                        else // if we didn't get our tape
                            _nav._destination = WaypointManager.singleton.GetExitWaypoint();
                        
                        ChangeState(AIState.Walking);
                    }
                    break;
                case AIState.WaitingInLine:
                    if (_timer > _patienceTime)
                    {
                        _timer = 0f;
                        
                        _nav._destination = WaypointManager.singleton.GetExitWaypoint();
                        ChangeState(AIState.Walking);
                    }
                    
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        switch (_currentState)
        {
            case AIState.Walking:
                break;
            case AIState.BrowsingTapes:
                browseAI();
                break;
            case AIState.WaitingInLine:
                lineAI();
                break;
        }
        
    }

    private void lineAI()
    {
        _timer += Time.deltaTime;
        
        
        
        // instantiate waiting thought bubble
    }

    private void browseAI()
    {
        _timer += Time.deltaTime;
        
        // check if our shelf has a tape
        // if so collect it
        
        // instantiate browsing thought bubble
    }

    private void ChangeState(AIState state)
    {
        _lastStateChange = Time.time;

        _currentState = state;
    }

    public AIState GetState() => _currentState;

    private void pickGenreToBuy() => _tapeToBuy = (Genre)UnityEngine.Random.Range(0, 6);

    private void CheckShelfStock()
    {
        new NotImplementedException();
    }
    
    private void FindWaypoint()
    {
        foreach (var waypoint in WaypointManager.singleton.shelves) // iterate through our shelves
        {
            if ((int)waypoint.type == (int)_tapeToBuy) // if the genres are the same
            {
                int randIndex = UnityEngine.Random.Range(0, waypoint.markers.Length);

                _nav._destination = waypoint.markers[randIndex];
                return;
            }
        }
    }
}


public enum AIState
{
    Walking,
    BrowsingTapes,
    WaitingInLine,
}
