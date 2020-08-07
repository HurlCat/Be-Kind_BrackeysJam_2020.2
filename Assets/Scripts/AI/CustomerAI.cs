using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    private CustNavigator _nav;
    private CustomerMood _mood;
    private Animator _animator;
    [SerializeField] private Shelf _shelf;
    [SerializeField] private Register _register;
    [SerializeField] private GameObject _GroundPrefab;

    private GameObject _moodBubbleInstance;
    
    [SerializeField] private Genre _tapeToBuy;
    [SerializeField] private bool _hasTape = false;
    [SerializeField] private bool _paid = false;
    [SerializeField] private bool _inLine = false;
    [SerializeField] private bool _leaving = false;
    
    [SerializeField] private int _desiredQuantity = 1;
    
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
        _register = FindObjectOfType<Register>();
        _mood = GetComponent<CustomerMood>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        RandomizeStats();
    }

    void Update()
    {
        Think();
    }

    void Think()
    {
        if(_nav._destination == null) // TODO: FIX THIS SCRIPT ORDERING PROBLEM
            FindWaypoint();

        if (Time.time > _lastStateChange + _stateDelay) // if we're able to change states again
        {
            switch (_currentState) // we're gonna try and see if we can change our state
            {
                case AIState.Walking:
                    _animator.SetBool("Walking", true);
                    
                    if (_nav._reachedEndOfPath) // if we get to our destination
                    {
                        _nav._destination = this.transform;

                        if (_leaving && this.transform.position.y < -2.5f)
                        {
                            Destroy(this.gameObject);
                        }
                        
                        if (_hasTape) // we came from getting our tapes
                            ChangeState(AIState.WaitingInLine);
                        else if (!_hasTape) // we haven't gotten a tape yet
                            ChangeState(AIState.BrowsingTapes);
                    }
                    break;
                case AIState.BrowsingTapes:
                    _animator.SetBool("Walking", false);
                    if (_timer > _browsingTime) // if we get tired of browsing
                    {
                        _timer = 0f;

                        if (_hasTape) // if we got our tape
                        {
                            _nav._destination = WaypointManager.singleton.GetRegisterWaypoint();
                            // TODO: INSTANTIATE HAPPY
                            Destroy(_moodBubbleInstance);
                            _mood.incrementMood(5);
                            _mood.CreateMoodBubble();

                        }
                        else // if we didn't get our tape
                        {
                            _nav._destination = WaypointManager.singleton.GetExitWaypoint();
                            // TODO: INSTANTIATE ANGRY
                            Destroy(_moodBubbleInstance);
                            _mood.decrementMood(5);
                            _mood.CreateMoodBubble();

                            _leaving = true;
                        }
                        
                        ChangeState(AIState.Walking);
                    }
                    break;
                case AIState.WaitingInLine:
                    _animator.SetBool("Walking", false);
                    if (_timer > _patienceTime) // customer lost patience
                    {
                        _timer = 0f;
                        
                        Destroy(_moodBubbleInstance);
                        _mood.decrementMood(8);
                        _mood.CreateMoodBubble();
                        
                        _nav._destination = WaypointManager.singleton.GetExitWaypoint();
                        _leaving = true;
                        
                        DropTape();

                        _register.LeaveLine(this);
                        ChangeState(AIState.Walking);
                    }
                    else if (_paid)// customer got serviced
                    {
                        _timer = 0f;
                        
                        // TODO: INSTANTIATE HAPPY
                        Destroy(_moodBubbleInstance);
                        _mood.incrementMood(5);
                        _mood.CreateMoodBubble();
                        
                        _nav._destination = WaypointManager.singleton.GetExitWaypoint();
                        _leaving = true;

                        _register.LeaveLine(this);
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
        
        // Queue in line
        if (!_inLine)
        {
            _register.GetInLine(this);
            _inLine = true;
        }
        
        // instantiate waiting thought bubble
        if(_moodBubbleInstance == null)
            _moodBubbleInstance = _mood.CreateStateBubble(_currentState);
    }

    private void browseAI()
    {
        _timer += Time.deltaTime;
        
        // check if our shelf has a tape
        if(!_hasTape)
            CheckShelfStock();
        
        // instantiate browsing thought bubble
        if(_moodBubbleInstance == null)
            _moodBubbleInstance = _mood.CreateStateBubble(_currentState);
    }

    private void ChangeState(AIState state)
    {
        _lastStateChange = Time.time;

        Destroy(_moodBubbleInstance, 1f);
        
        _currentState = state;
    }

    public AIState GetState() => _currentState;

    private void RandomizeStats()
    {
        _tapeToBuy = (Genre)UnityEngine.Random.Range(0, 6);
        _patienceTime = UnityEngine.Random.Range(3f, 5f);
        _browsingTime = UnityEngine.Random.Range(3f, 8f);
        _desiredQuantity = UnityEngine.Random.Range(1, 2);

    } 

    private void CheckShelfStock()
    {
        if (_shelf.takeFromShelf(_desiredQuantity))
            _hasTape = true;
    }
    
    private void FindWaypoint()
    {
        foreach (var waypoint in WaypointManager.singleton.shelves) // iterate through our shelves
        {
            if ((int)waypoint.type == (int)_tapeToBuy) // if the genres are the same
            {
                _shelf = waypoint.shelf;
                
                int randIndex = UnityEngine.Random.Range(0, waypoint.markers.Length);

                _nav._destination = waypoint.markers[randIndex];
                return;
            }
        }
    }

    public void PayForTape() => _paid = true;
    
    private void DropTape()
    {
        GameObject _groundTape = (GameObject)Instantiate(_GroundPrefab, this.transform.position, Quaternion.identity);
        _groundTape.GetComponent<GroundTape>().SetTape(new VHSTape(_tapeToBuy, true));
    }
}


public enum AIState
{
    Walking,
    BrowsingTapes,
    WaitingInLine,
}
