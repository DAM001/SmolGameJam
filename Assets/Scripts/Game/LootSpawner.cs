using UnityEngine;
using System;

[Serializable]
public class Loot
{
    [SerializeField] private GameObject[] _lootArray;
    [SerializeField] private float _probability = 100f;

    public float Probability
    {
        get => _probability;
        set => _probability = value;
    }

    public int NumberOfLootType
    {
        get => _lootArray.Length;
    }

    public GameObject RandomLoot
    {
        get
        {
            int index = UnityEngine.Random.Range(0, _lootArray.Length);
            return _lootArray[index];
        }
    }
}

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private Loot[] _loot;

    private void Start()
    {
        if (_loot.Length <= 1) return;

        for (int i = 1; i < _loot.Length; i++)
        {
            _loot[i].Probability += _loot[i - 1].Probability;
        }
    }

    public GameObject[] Spawn(int numberOfSpawn)
    {
        if (numberOfSpawn <= 0) return null;

        GameObject[] lootArray = new GameObject[numberOfSpawn];

        for (int i = 0; i < numberOfSpawn; i++)
        {
            float currentProbability = UnityEngine.Random.Range(0f, AllLootProbability());

            bool created = false;
            for (int j = _loot.Length - 1; j >= 0; j--)
            {
                if (!created && currentProbability > (_loot[j].Probability - _loot[0].Probability))
                {
                    lootArray[i] = CreateItem(_loot[j]);
                    created = true;
                }
            }
        }

        return lootArray;
    }

    private float AllLootProbability()
    {
        float allLootProbability = 0f;
        for (int i = 0; i < _loot.Length; i++)
        {
            allLootProbability += _loot[i].Probability;
        }

        return allLootProbability;
    }

    private GameObject CreateItem(Loot loot)
    {
        float distance = 1f;
        GameObject item = Instantiate(loot.RandomLoot);

        item.transform.parent = null;
        item.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-distance, distance), 1f, UnityEngine.Random.Range(-distance, distance));
        return item;
    }
}
