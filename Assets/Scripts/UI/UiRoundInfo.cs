using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRoundInfo : MonoBehaviour
{
    [SerializeField] private Text _alive;
    [SerializeField] private Text _damage;
    [SerializeField] private Text _kills;

    private float _damageVal = 0f;
    private int _killsVal = 0;

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

    private void FixedUpdate()
    {
        if (Data.Characters.Count == 1)
        {
            Data.Characters[0].GetComponent<UnitHealth>().UseHeal();
        }
    }

    public void Damage(float value)
    {
        _damageVal += value;
        _damage.text = "Damage: " + _damageVal.ToString("0");
    }

    public void Kills(int kills)
    {
        _killsVal += kills;
        _kills.text = "Kills: " + _killsVal.ToString("0");
    }
}
