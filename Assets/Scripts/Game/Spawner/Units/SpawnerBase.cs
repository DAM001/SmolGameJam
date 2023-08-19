using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    public GameObject[] CreateMultipleObjects(int quantity, GameObject spawnObject)
    {
        GameObject[] spawnedObjects = new GameObject[quantity];

        for (int i = 0; i < quantity; i++)
        {
            spawnedObjects[i] = CreateObject(spawnObject);
        }

        return spawnedObjects;
    }

    public GameObject[] CreateMultipleObjects(int quantity, GameObject[] spawnObjects)
    {
        GameObject[] spawnedObjects = new GameObject[quantity];

        for (int i = 0; i < quantity; i++)
        {
            spawnedObjects[i] = CreateRandomObject(spawnObjects);
        }

        return spawnedObjects;
    }

    public GameObject CreateObject(GameObject spawnObject)
    {
        GameObject spawnedObject = Instantiate(spawnObject, transform);
        spawnedObject.transform.parent = null;
        return spawnedObject;
    }

    public GameObject CreateRandomObject(GameObject[] spawnObjects)
    {
        return CreateObject(spawnObjects[Random.Range(0, spawnObjects.Length)]);
    }
}
