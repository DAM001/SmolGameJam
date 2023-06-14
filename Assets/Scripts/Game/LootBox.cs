using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private LootSpawner _spawner;
    [SerializeField] private int _numberOfLoot = 4;

    public void Start()
    {
        GameObject[] items = _spawner.Spawn(_numberOfLoot);

        for (int i = 0; i < items.Length; i++)
        {
            float distance = 3f;
            items[i].transform.position = transform.position + new Vector3(Random.Range(-distance, distance), 5f, Random.Range(-distance, distance));
        }
    }
}
