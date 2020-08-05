using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSortingLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] bool _CustomYOffset;
    [SerializeField] float yOffset;

    [SerializeField] private float offset;
    
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_CustomYOffset)
        {
            offset = _spriteRenderer.sprite.bounds.size.y / 2;
            if (_spriteRenderer.sortingOrder != Mathf.RoundToInt(this.transform.position.y - (yOffset))) // if the sorting order isn't the same as y
                _spriteRenderer.sortingOrder = -Mathf.RoundToInt(this.transform.position.y - (yOffset)); // set it to be the same as y
        }
        else if (_spriteRenderer.sortingOrder != Mathf.RoundToInt(this.transform.position.y - (_spriteRenderer.sprite.bounds.size.y / 2)))
            _spriteRenderer.sortingOrder = -Mathf.RoundToInt(this.transform.position.y - (_spriteRenderer.sprite.bounds.size.y / 2));
    }
}
