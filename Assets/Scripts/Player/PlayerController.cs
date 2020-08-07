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
    
    [Header("Sounds")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _stockShelf;

    [SerializeField] private float _throwForce = 5f;
    
    [Header("InteractField")]
    [SerializeField] private Transform interactCheck;
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask whatIsInteractable;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _inventory = GetComponent<PlayerInventory>();
        _audioSource = GetComponent<AudioSource>();
    } 

    private void Update()
    {
        _animator.SetFloat("VertMove", _playerMovement.vertInput);
        _animator.SetFloat("HorizMove", _playerMovement.horizInput);
        _animator.SetFloat("Speed", new Vector2(_playerMovement.vertInput, _playerMovement.horizInput).magnitude);

        if (!(_playerMovement.vertInput == 0f && _playerMovement.horizInput == 0f))
        {
            _animator.SetFloat("OldVert", _playerMovement.oldVert);
            _animator.SetFloat("OldHoriz", _playerMovement.oldHoriz);
        }
            
        
        if(Input.GetButtonDown("Fire1"))
            Interact();

        if (Input.GetButtonDown("Fire2") && _inventory.GetInventorySize() > 0)
            ThrowTape();
    }
    
    void Interact()
    {
        var hit = Physics2D.OverlapCircle(interactCheck.position, interactRadius, whatIsInteractable);

        if (!hit)
            return;
        
        switch (hit.tag)
        {
            case "Shelf":
                if( _inventory.StockShelf(hit) )
                    Util.PlayAudio(_audioSource, _stockShelf);
                break;
            case "VHSBin":
                _inventory.GiveRandomTape();
                break;
            case "Register" :
                hit.GetComponent<Register>().TakePayment();
                break;
            case "VHSRewinder":
                _inventory.RewindTape(hit);
                break;
        }
    }

    void ThrowTape()
    {
        GameObject tape = _inventory.DropTape(_inventory.GetInventorySize() - 1, interactCheck);
        Rigidbody2D rb = tape.GetComponent<Rigidbody2D>();

        Vector2 direction = (interactCheck.position - this.transform.position).normalized;
        
        rb.AddForce(direction * _throwForce);
    }
}
