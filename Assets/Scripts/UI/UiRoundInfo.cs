using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRoundInfo : MonoBehaviour
{
    [SerializeField] private Text _alive;

    private void FixedUpdate()
    {
        _alive.text = "Alive: " + Data.Characters.Count;
    }
}
