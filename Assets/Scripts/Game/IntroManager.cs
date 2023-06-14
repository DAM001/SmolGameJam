using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject _introScene;
    [SerializeField] private GameObject _gameScene;
    [SerializeField] private CameraIntro _camera;
    [SerializeField] private GameObject _microscope;

    public void Start()
    {
        _introScene.SetActive(true);
        _gameScene.SetActive(false);

        _camera.SetTarget(_microscope);
    }

    public void OnGameStart()
    {
        _introScene.SetActive(false);
        _gameScene.SetActive(true);

        _camera.SetTarget(null);
    }

    public void StartAnim()
    {
        _camera.Move();
    }
}
