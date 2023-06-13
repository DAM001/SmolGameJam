using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static List<GameObject> Items = new List<GameObject>();
    public static List<GameObject> Characters = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1f);

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            Data.Items.Add(items[i]);
        }

        Characters.Add(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < players.Length; i++)
        {
            Data.Characters.Add(players[i]);
        }
    }

    /*private void FixedUpdate()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] == null) Items.Remove(Items[i]);
        }

        for (int i = 0; i < Characters.Count; i++)
        {
            if (Items[i] == null) Characters.Remove(Items[i]);
        }
    }*/
}
