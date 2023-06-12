using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _loots;

    public GameObject[] Spawn(int numberOfSpawn)
    {
        GameObject[] loots = new GameObject[numberOfSpawn];

        for (int i = 0; i < numberOfSpawn; i++)
        {
            loots[i] = Instantiate(_loots[Random.Range(0, _loots.Length)]);
            loots[i].transform.parent = null;
            float distance = 1f;
            loots[i].transform.position = transform.position + new Vector3(Random.Range(-distance, distance), 1f, Random.Range(-distance, distance));
        }

        return loots;
    }
}
