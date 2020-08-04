using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSortingLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRenderer.sortingOrder != Mathf.RoundToInt(this.transform.position.y - (_spriteRenderer.sprite.bounds.size.y / 2)))
        {
            _spriteRenderer.sortingOrder = -Mathf.RoundToInt(this.transform.position.y - (_spriteRenderer.sprite.bounds.size.y / 2));
            
        }
    }
}
