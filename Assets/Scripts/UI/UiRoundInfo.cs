using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRoundInfo : MonoBehaviour
{
    [SerializeField] private Text _alive;

    private void Start()
    {
        StartCoroutine(UpdateHandler());
    }

    private IEnumerator UpdateHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < Data.Characters.Count; i++)
            {
                if (Data.Characters[i] == null) Data.Characters.Remove(Data.Characters[i]);
            }
            _alive.text = "Alive: " + (Data.Characters.Count);
        }
    }
}
