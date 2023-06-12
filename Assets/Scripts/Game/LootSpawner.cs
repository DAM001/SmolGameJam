using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _ammo;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private GameObject[] _other;

    public GameObject[] Spawn(int numberOfSpawn)
    {
        GameObject[] loots = new GameObject[numberOfSpawn];

        /*for (int i = 0; i < numberOfSpawn; i++)
        {
            loots[i] = Instantiate(_loots[UnityEngine.Random.Range(0, _loots.Length)]);
            loots[i].transform.parent = null;
            float distance = 1f;
            loots[i].transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-distance, distance), 1f, UnityEngine.Random.Range(-distance, distance));
        }*/

        for (int i = 0; i < numberOfSpawn; i++)
        {
            if (i < numberOfSpawn / 2) loots[i] = CreateItem(_ammo);
            else if ((i < numberOfSpawn / 2 + numberOfSpawn / 3)) loots[i] = CreateItem(_weapons);
            else loots[i] = CreateItem(_other);
        }

        return loots;
    }

    private GameObject CreateItem(GameObject[] items)
    {
        GameObject item = Instantiate(items[UnityEngine.Random.Range(0, items.Length)]);
        item.transform.parent = null;
        float distance = 1f;
        item.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-distance, distance), 1f, UnityEngine.Random.Range(-distance, distance));
        return item;
    }
}
