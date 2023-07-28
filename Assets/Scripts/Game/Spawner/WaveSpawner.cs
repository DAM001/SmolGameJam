using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveObjects
{
    public GameObject SpawnObject;
    public int Quantity;
    public bool CountInTheWave;
}

[Serializable]
public class Wave
{
    public WaveObjects[] WaveData;
}

public class WaveSpawner : SpawnerBase
{
    [SerializeField] private Wave[] _waves;

    private int _currentWave = 0;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void FixedUpdate()
    {
        if (_spawnedObjects.Count == 0)
        {
            if (_waves.Length >= _currentWave) return;

            StartWave();
            _currentWave++;
        }

        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            if (_spawnedObjects[i] == null)
            {
                _spawnedObjects.RemoveAt(i);
            }
        }
    }

    public void StartWave()
    {
        for (int i = 0; i < _waves[_currentWave].WaveData.Length; i++)
        {
            WaveObjects data = _waves[_currentWave].WaveData[i];
            GameObject[] spawnedObjects = CreateMultipleObjects(data.Quantity, data.SpawnObject);

            for (int j = 0; j < spawnedObjects.Length; j++)
            {
                _spawnedObjects.Add(spawnedObjects[j]);
            }
        }
    }
}
