using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _spectateScreen;
    [SerializeField] private GameObject _plyerInfo;

    public void MainMenu()
    {
        SetScreen(1);
        HideMenu();
    }

    public void HideMenu()
    {
        _mainMenuScreen.SetActive(false);
    }

    public void GameScreen()
    {
        _plyerInfo.SetActive(true);
        _spectateScreen.SetActive(false);
        Cursor.visible = false;

        SetScreen(1);
    }

    public void GameOver()
    {
        SetScreen(2);
    }

    public void Spectate()
    {
        Cursor.visible = true;
        GameObject.FindGameObjectWithTag("Cursor").SetActive(false);
        _plyerInfo.SetActive(false);
        _spectateScreen.SetActive(true);
    }

    private void SetScreen(int idx)
    {
        _mainMenuScreen.SetActive(idx == 0);
        _gameScreen.SetActive(idx == 1);
        _gameOverScreen.SetActive(idx == 2);
    }
}
