using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager _uiManager;
    [SerializeField] private Data _data;
    [SerializeField] private IntroManager _introManager;
    [Space(10)]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playersSpawner;
    [SerializeField] private int _numberOfPlayers = 50;

    private void Start()
    {
        _uiManager.MainMenu();
    }

    public void StartGame()
    {
        _uiManager.HideMenu();

        StartCoroutine(IntroAnim());
    }

    private IEnumerator IntroAnim()
    {
        _introManager.StartAnim();

        yield return new WaitForSeconds(0f);

        _uiManager.GameScreen();
        _data.SetupData();
        _introManager.OnGameStart();

        GameObject player = Instantiate(_player, transform);
        player.transform.parent = null;

        _playersSpawner.GetComponent<LootGenerator>().Spawn(_numberOfPlayers);
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
