using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameObjectUtil : MonoBehaviour
{
    public static GameObject FindClosest(GameObject[] objects, Vector3 position)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                Vector3 diff = obj.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = obj;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    public static GameObject[] FindObjectsByTpye(InventoryItemType itemType, GameObject[] objects)
    {
        List<GameObject> targetOjects = new List<GameObject>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetComponent<InventoryItem>().ItemType == itemType)
            {
                targetOjects.Add(objects[i]);
            }
        }

        return targetOjects.ToArray();
    }

    public static Vector3 GetRandomPositionInCircle(float range)
    {
        return new Vector3(Random.Range(-range, range), 0f, Random.Range(-range, range));
    }

    public static Vector3 GetRandomPositionInSphere(float range)
    {
        return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
    }
}
