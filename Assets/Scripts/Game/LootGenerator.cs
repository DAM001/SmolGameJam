using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _loot;
    [SerializeField] private int _numberOfLoot = 30;
    [SerializeField] private float _distance = 150f;
    [SerializeField] private bool _rotate = false;

    private void Start()
    {
        for (int i = 0; i < _numberOfLoot; i++)
        {
            CreateItem();
        }
    }

    public void Spawn(int number)
    {
        _numberOfLoot = number;

        for (int i = 0; i < _numberOfLoot; i++)
        {
            CreateItem();
        }
    }

    private GameObject CreateItem()
    {
        GameObject item = Instantiate(_loot);
        item.transform.parent = null;
        item.transform.Rotate(0, Random.Range(-1000f, 1000f), 0);
        item.transform.position = transform.position + item.transform.forward * Random.Range(15f, _distance);
        if (_rotate) item.transform.GetChild(0).Rotate(0f, Random.Range(0f, 360f), 0f);
        return item;
    }
}
