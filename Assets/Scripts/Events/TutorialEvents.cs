using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents singleton;
    
    public event Action onFirstUnrewound, onFirstLowStock, onFirstStart, onFirstCustomer, onFirstAngry;

    private void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;
    }

    public void FirstUnrewound()
    {
        onFirstUnrewound?.Invoke();
    }

    public void FirstLowStock()
    {
        onFirstLowStock?.Invoke();
    }

    public void FirstStart()
    {
        onFirstStart?.Invoke();
    }

    public void FirstCustomer()
    {
        onFirstCustomer?.Invoke();
    }

    public void FirstAngry()
    {
        onFirstStart?.Invoke();
    }
}
