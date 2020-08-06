using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] CustomerPrefabs;
    [SerializeField] private float _maxSpawnRateInSeconds = 5f;
    [SerializeField] private float _minSpawnRateInSeconds = 1f;

    [SerializeField] private float _increaseSpawnRateInSeconds = 15f;

    [Header("SpawnArea")]
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;
    
    void Start()
    {
        Invoke("SpawnCustomer", _maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, _increaseSpawnRateInSeconds);
    }

    void SpawnCustomer()
    {
        Vector2 position = new Vector2(Random.Range(_min.x, _max.x), Random.Range(_min.y, _max.y));
        Instantiate(CustomerPrefabs[UnityEngine.Random.Range(0, CustomerPrefabs.Length)], position, Quaternion.identity);
        ScheduleNextCustomerSpawn();
    }

    void ScheduleNextCustomerSpawn()
    {
        float spawnInSeconds;
        if (_maxSpawnRateInSeconds > 1f)
            spawnInSeconds = Random.Range(1f, _maxSpawnRateInSeconds);
        else
            spawnInSeconds = 1f;

        Invoke("SpawnCustomer", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (_maxSpawnRateInSeconds > _minSpawnRateInSeconds)
            _maxSpawnRateInSeconds--;
        
        if(_maxSpawnRateInSeconds <= _minSpawnRateInSeconds)
            CancelInvoke("IncreaseSpawnRate");
    }
}
