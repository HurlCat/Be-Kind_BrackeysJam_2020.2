using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<VHSTape> _inventory = new List<VHSTape>();
    private List<GameObject> _UITapes = new List<GameObject>();
    public List<Transform> _UISlots = new List<Transform>(2);
    
    [SerializeField]
    private GameObject _UIPrefab, _GroundPrefab;
    
    internal void GiveRandomTape()
    {
        if (_inventory.Count >= 2)
            DropTape(_inventory.Count-1);

        List<int> genresInStock = VHSLibrary.singleton.GenresInStock();
        if (genresInStock.Count == 0)
            return;                                                     //TODO: TELL USER OUT OF STOCK

        int rand = UnityEngine.Random.Range(0, genresInStock.Count);
        
        _inventory.Add(VHSLibrary.singleton.GetTapeFromGenre(genresInStock[rand])); // grabs a tape from the bin
        
        InstantiateNewestTape(_inventory.Count-1);

        Debug.Log("Added new tape " + _inventory[_inventory.Count-1].genre);
    }

    internal bool StockShelf(Collider2D shelf)
    {
        Shelf shelfInfo = shelf.GetComponent<Shelf>();

        for (int i = 0; i < _inventory.Count; i++)
            if (shelfInfo.CompareGenres(_inventory[i].genre) && _inventory[i].rewound && !shelfInfo.IsFull()) // if the tapes are the same genre, rewound, and we have room
            {
                Debug.Log("Tape Returned");
                
                shelfInfo.IncrementStock(); // stock shelf
                
                RemoveTapeFromInventory(i);
                
                return true; // exit
            }
        return false;
    }

    internal void RewindTape(Collider2D rewinder)
    {
        TapeRewinder tapeRewinder = rewinder.GetComponent<TapeRewinder>();

        if (tapeRewinder.IsRewinding() && tapeRewinder.IsFull()) // if the thing is rewinding a tape
            return;
        
        if (!tapeRewinder.IsRewinding() && tapeRewinder.IsFull()) // if the thing is done rewinding a tape
        {
            if (_inventory.Count == 2)
                DropTape(_inventory.Count-1);
            
            _inventory.Add(tapeRewinder.GiveTapeToPlayer());
            InstantiateNewestTape(_inventory.Count-1);
            return;
        }
        else // not rewinding and not full
        {
            for(int i = 0; i < _inventory.Count; i++) // iterate through inventory
                if (!_inventory[i].rewound) // if we find an unwound tape
                {
                    tapeRewinder.InsertTape(_inventory[i]); // insert the tape into the rewinder
                    
                    RemoveTapeFromInventory(i);
                    return;
                }
        }
        
        Debug.Log("No rewindable tapes");
    }

    private void RemoveTapeFromInventory(int index)
    {
        _inventory.Remove(_inventory[index]); // remove from inventory
                
        Destroy(_UITapes[index]); // destroy UI element
        _UITapes.Remove(_UITapes[index]); // remove reference in list

        foreach (var tape in _UITapes)
        {
            tape.transform.SetParent(_UISlots[Mathf.Clamp(_UISlots.IndexOf(tape.transform.parent) - 1, 0, 1)]);
            tape.transform.position = tape.transform.parent.position;
        }
    }

    private void InstantiateNewestTape(int index)
    {
        GameObject tapeUIPrefab = (GameObject) Instantiate(_UIPrefab, _UISlots[index]); // instantiate the UI Element
        
        tapeUIPrefab.name = _inventory[_inventory.Count - 1].genre.ToString() + "_Tape"; // rename the UI Element
        tapeUIPrefab.GetComponent<VHSUI>().tape = _inventory[_inventory.Count-1]; // set the Element's graphics
        
        _UITapes.Add(tapeUIPrefab); 
    }

    internal GameObject DropTape(int index)
    {
        GameObject _groundTape = (GameObject)Instantiate(_GroundPrefab, this.transform.position, Quaternion.identity);
        _groundTape.GetComponent<GroundTape>().SetTape(_inventory[index]);
        
        RemoveTapeFromInventory(index);

        return _groundTape;
    }
    
    internal GameObject DropTape(int index, Transform spawnPoint)
    {
        GameObject _groundTape = (GameObject)Instantiate(_GroundPrefab, spawnPoint.position, Quaternion.identity);
        _groundTape.GetComponent<GroundTape>().SetTape(_inventory[index]);
        
        RemoveTapeFromInventory(index);

        return _groundTape;
    }

    public int GetInventorySize() => _inventory.Count;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tape"))
        {
            if (_inventory.Count < 2)
            {
                _inventory.Add(other.gameObject.GetComponent<GroundTape>().GiveTapeToPlayer());
                InstantiateNewestTape(_inventory.Count-1);
                Destroy(other.gameObject);
            }
        }
    }
}
