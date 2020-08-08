using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfAnimator : MonoBehaviour
{
    [SerializeField] private Shelf _shelf;
    private Animator _animator;

    void Awake() => _animator = GetComponent<Animator>();
    
    public void UpdateSprite(float shelfStatus)
    {
        if (shelfStatus >= .80f)
        {
            _animator.Play("Full");
        } 
        else if (shelfStatus >= .40f)
        {
            _animator.Play("HalfFull");
        }
        else if (shelfStatus >= .1f)
        {
            _animator.Play("AlmostEmpty");
        }
        else if (shelfStatus >= 0f)
        {
            _animator.Play("Empty");
        }
    }
}
