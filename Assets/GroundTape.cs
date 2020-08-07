using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTape : MonoBehaviour
{
    public VHSTape tape;
    public SpriteRenderer tapeCover;
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        Invoke("EnableCollisions", .05f);
    }

    public void SetTape(VHSTape tape)
    {
        this.tape = tape;
        tapeCover.sprite = tape.graphics;
    }

    public VHSTape GiveTapeToPlayer() => tape;

    private void EnableCollisions() => _collider.enabled = true;
}
