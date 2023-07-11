using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager _uiManager;
    [SerializeField] private Data _data;
    [Space(10)]
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _uiManager.MainMenu();

        //StartGame();
    }

    public void StartGame()
    {
        _uiManager.HideMenu();

        StartCoroutine(IntroAnim());
    }

    private IEnumerator IntroAnim()
    {
        yield return new WaitForSeconds(0f);

        _uiManager.GameScreen();
        _data.SetupData();


        //GameObject player = Instantiate(_player, transform);
        //player.transform.parent = null;
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
