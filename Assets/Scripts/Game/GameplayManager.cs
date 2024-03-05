using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _npcs;
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] private GameObject[] _ligths;

    private void Start()
    {
        StartCoroutine(StartSpawnMonsters());
    }

    private IEnumerator StartSpawnMonsters()
    {
        yield return new WaitForSeconds(20);
        _spawner.SetActive(true);
        _npcs.SetActive(false);

        _musicManager.PlayMusicById(1);

        _ligths[0].SetActive(false);
        _ligths[1].SetActive(true);
    }
}
