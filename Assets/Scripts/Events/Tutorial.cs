using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial singleton;
    
    private bool _stock, _start, _customer, _angry, _tutActive = false;

    private int _lastTutRan;

    [SerializeField] private Camera _tutorialCamera;
    [SerializeField] private GameObject[] _tutUI = new GameObject[4];

    private void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;

        TutorialEvents.singleton.onFirstCustomer += FirstCustomer; // Add event listeners
        TutorialEvents.singleton.onFirstLowStock += FirstStock;
        TutorialEvents.singleton.onFirstStart += FirstStartAlert;
        TutorialEvents.singleton.onFirstAngry += FirstAngry;

        
    }

    private void Start()
    {
        _tutUI = TutorialEvents.singleton.tutUI;
        FirstStartAlert();
    }

    private void Update()
    {
        if (_tutActive)
            if (Input.anyKeyDown)
                CloseTutorial();
    }

    private void FirstAngry(CustomerMood customer)
    {
        if (!_angry) // only happens if it's our first time encountering it
        {
            RunTutorial(customer.transform, 0);
            _angry = true;
        }
    }

    private void FirstStartAlert()
    {
        if (!_start)
        {
            RunTutorial(Camera.main.transform, 1, 3.5f);
            _start = true;
        }
    }

    private void FirstStock(Shelf shelf)
    {
        if (!_stock)
        {
            RunTutorial(shelf.transform, 2);
            _stock = true;
        }
    }

    void FirstCustomer(CustomerMood customer)
    {
        if (!_customer)
        {
            RunTutorial(customer.transform, 3);
            _customer = true;
        }
    }

    void RunTutorial(Transform transform, int tutorialToRun, float cameraSize = 1.5f)
    {
        StartCoroutine(nameof(EnableClosing));
        _lastTutRan = tutorialToRun;

        _tutorialCamera.orthographicSize = cameraSize;
        _tutorialCamera.gameObject.SetActive(true);
        _tutUI[tutorialToRun].SetActive(true);
        GameController.singleton.TogglePause();
            
        Vector3 position = new Vector3(transform.position.x, transform.position.y, _tutorialCamera.transform.position.z);
        _tutorialCamera.transform.position = position;
            
        _stock = true;
    }

    IEnumerator EnableClosing()
    {
        yield return new WaitForSecondsRealtime(2f);
        _tutActive = true;
    }
    
    void CloseTutorial()
    {
        _tutActive = false;
        
        _tutUI[_lastTutRan].SetActive(false);
        _tutorialCamera.gameObject.SetActive(false);
        GameController.singleton.TogglePause();
    }
}
