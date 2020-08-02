using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<VHSTape> _inventory = new List<VHSTape>();
    private List<GameObject> _UITapes = new List<GameObject>();
    public List<Transform> _UISlots = new List<Transform>(2);
    
    [SerializeField]
    private Transform _UIElement;

    [SerializeField]
    private GameObject _UIPrefab;

    internal void GiveTape()
    {
        if (_inventory.Count >= 2)
        {
            //drop a tape
            _inventory.RemoveAt(1);
            Destroy(_UITapes[1]);
            _UITapes.RemoveAt(1);
            
            Debug.Log("Tape Removed");
        }
        
        _inventory.Add(new VHSTape((Genre)UnityEngine.Random.Range(0,5), true));
        GameObject tape = (GameObject) Instantiate(_UIPrefab, _UIElement);
        tape.name = _inventory[_inventory.Count - 1].genre.ToString() + "_Tape";
        tape.GetComponent<VHSUI>().tape = _inventory[_inventory.Count-1];
        
        _UITapes.Add(tape);
        
        Debug.Log("Added new tape " + _inventory[_inventory.Count-1].genre);
    }
    
    internal void StockShelf(Collider2D shelf)
    {
        Shelf shelfInfo = shelf.GetComponent<Shelf>();

        for (int i = 0; i < _inventory.Count; i++)
            if (shelfInfo.CompareGenres(_inventory[i].genre) && _inventory[i].rewound && !shelfInfo.IsFull()) // if the tapes are the same genre, rewound, and we have room
            {
                Debug.Log("Tape Returned");
                
                shelfInfo.IncrementStock(); // stock shelf
                _inventory.Remove(_inventory[i]); // remove from inventory
                Destroy(_UITapes[i]);
                _UITapes.Remove(_UITapes[i]); // remove UI element
                return; // exit
            }
            
        Debug.Log("No Tapes Found");
    }
}
