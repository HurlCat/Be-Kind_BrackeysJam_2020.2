using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodBubble : MonoBehaviour
{
    [SerializeField] private float _lifetime = 2f;
    private void Start() => Invoke("DestroyBubble", _lifetime);

    private void DestroyBubble() => Destroy(gameObject);
}
