using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents singleton;
    public delegate void StockCallback(Shelf shelf);
    public delegate void CusstomerCallback(CustomerMood customer);

    public GameObject[] tutUI = new GameObject[4];
    
    public event Action onFirstStart;
    public event StockCallback onFirstLowStock;
    public event CusstomerCallback onFirstCustomer, onFirstAngry;
    
    private void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;
    }

    public void FirstLowStock(Shelf shelf)
    {
        onFirstLowStock?.Invoke(shelf);
        
    }

    public void FirstStart()
    {
        onFirstStart?.Invoke();
    }

    public void FirstCustomer(CustomerMood customer)
    {
        onFirstCustomer?.Invoke(customer);
    }

    public void FirstAngry(CustomerMood customer)
    {
        onFirstAngry?.Invoke(customer);
    }
}
