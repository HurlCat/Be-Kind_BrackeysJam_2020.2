using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial singleton;
    
    private bool _rewind, _stock, _start, _customer, _angry = false;

    private void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;

        TutorialEvents.singleton.onFirstCustomer += FirstCustomer; // Add event listeners
        TutorialEvents.singleton.onFirstLowStock += FirstStock;
        TutorialEvents.singleton.onFirstStart += FirstStartAlert;
        TutorialEvents.singleton.onFirstUnrewound += FirstRewind;
        TutorialEvents.singleton.onFirstAngry += FirstAngry;
    }

    private void FirstAngry()
    {
        if (!_angry) // only happens if it's our first time encountering it
        {
            throw new NotImplementedException();
        }
    }

    private void FirstRewind()
    {
        if (!_rewind)
        {
            throw new NotImplementedException();
        }
    }

    private void FirstStartAlert()
    {
        if (!_start)
        {
            throw new NotImplementedException();
        }
    }

    private void FirstStock()
    {
        if (!_stock)
        {
            Debug.Log("HEY YOU HAVE LOW STOCK");
            _stock = true;
        }
    }

    void FirstCustomer()
    {
        if (!_customer)
        {
            throw new NotImplementedException();
        }
    }
}
