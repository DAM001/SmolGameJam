using System.Collections;
using System.Collections.Generic;
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
}
