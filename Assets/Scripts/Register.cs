using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Register : MonoBehaviour
{
    private List<CustomerAI> _customerQueue = new List<CustomerAI>();

    [SerializeField] private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    public void TakePayment()
    {
        if (_customerQueue.Count > 0)
            _customerQueue.First().PayForTape();
        else return;

    }

    public void GetInLine(CustomerAI customer)
    {
        _customerQueue.Add(customer);
    }

    public void LeaveLine(CustomerAI customer)
    {
        _customerQueue.Remove(customer);
    }
}
