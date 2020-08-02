using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    internal PlayerMovement _playerMovement;
    internal PlayerInventory _inventory;
    
    [Header("InteractField")]
    [SerializeField] private Transform interactCheck;
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask whatIsInteractable;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _inventory = GetComponent<PlayerInventory>();
    } 

    private void Update()
    {
        _animator.SetFloat("VertMove", _playerMovement.vertInput);
        _animator.SetFloat("HorizMove", _playerMovement.horizInput);
        
        if(Input.GetButtonDown("Fire1"))
            Interact();

        if (Input.GetButtonDown("Fire2"))
            RewindTape();
    }

    private void RewindTape()
    {
        throw new NotImplementedException();
    }

    void Interact()
    {
        var hit = Physics2D.OverlapCircle(interactCheck.position, interactRadius, whatIsInteractable);

        if (!hit)
            return;
        
        switch (hit.tag)
        {
            case "Shelf":
                _inventory.StockShelf(hit);
                // AddScore();
                break;
            case "VHSBin":
                _inventory.GiveRandomTape();
                break;
            case "Customer" :
                // if customer is returning
                    // GiveTape();
                    // AddScore();
                // if customer is checking out
                    // AddScore();
                break;
        }
    }
}
