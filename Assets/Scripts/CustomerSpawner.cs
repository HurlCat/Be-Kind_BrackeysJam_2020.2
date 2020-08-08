using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] CustomerPrefabs;
    private float _maxSpawnRateInSeconds = 5f;
    private float _minSpawnRateInSeconds = 1f;

    [Header("SpawnArea")]
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;
    
    void Start()
    {
        Vector2 spawnRates = GameController.singleton.settings.GetCustomerSpawnRate();

        _minSpawnRateInSeconds = spawnRates.x;
        _maxSpawnRateInSeconds = spawnRates.y;
        
        Invoke("SpawnCustomer", 15f);
        InvokeRepeating("IncreaseSpawnRate", 0f, GameController.singleton.settings.increaseSpawnRate);
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
