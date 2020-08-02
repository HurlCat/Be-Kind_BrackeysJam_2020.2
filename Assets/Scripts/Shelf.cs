using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int _capacity;
    private int _tapesInStock;

    [SerializeField] private Genre _genre;

    public void IncrementStock() => _tapesInStock++;
    public bool CompareGenres(Genre genre) => (genre == _genre) ? true : false; // returns true if param == this.genre

    public bool IsFull() => _tapesInStock >= _capacity; // returns true if at capacity
}