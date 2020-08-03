using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    internal float horizInput;
    internal float vertInput;

    internal float oldVert;
    internal float oldHoriz;

    [Header("PlayerStats")]
    [SerializeField] private float walkSpeed = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        oldHoriz = horizInput;
        oldVert = vertInput;
        
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        _rb.MovePosition(this.transform.position + new Vector3(horizInput, vertInput, 0f)
                         * (Time.deltaTime * walkSpeed));
    }
}
