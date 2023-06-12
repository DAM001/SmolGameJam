using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CircleData
{
    public float RestTime = 5f;
    public float ShrinkTime = 5f;
    public float Damage = 1f;
}

public class Circle : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private CircleData[] _data;

    private Vector3 _targetPos = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;
    private float _height = 320f;
    private float _currentLevel = 0f;
    private int _currentStep = 0;
    private bool _move = false;
    private float _timeToMove = 0f;

    private void Start()
    {
        transform.position = new Vector3(_targetPos.x, _currentLevel, _targetPos.z);

        StartCoroutine(CircleHandler());
    }

    private void FixedUpdate()
    {
        if (!_move) return;
        _currentLevel += (_height / _data.Length) / _data[_currentStep].ShrinkTime * Time.fixedDeltaTime;
        _timeToMove += Time.deltaTime / _data[_currentStep].ShrinkTime;
        transform.position = Vector3.Lerp(_startPosition, new Vector3(_targetPos.x, _currentLevel, _targetPos.z), _timeToMove);

    }

    public void SetDestination(Vector3 destination)
    {
        _timeToMove = 0;
        _startPosition = transform.position;
        _targetPos = destination;
    }

    private IEnumerator CircleHandler()
    {
        while(_currentStep < _data.Length)
        {
            float size = (_height - _currentLevel) / 3f;
            SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-size, size), 0f, UnityEngine.Random.Range(-size, size)));
            Notify("More soap in " + _data[_currentStep].RestTime + " seconds!");
            yield return new WaitForSeconds(_data[_currentStep].RestTime);
            _move = true;
            Notify("Soap is coming!");
            yield return new WaitForSeconds(_data[_currentStep].ShrinkTime);
            _move = false;
            _currentStep++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player" || tag == "Character")
        {
            other.gameObject.GetComponent<CharacterHealth>().InCircle(true, _data[_currentStep].Damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player" || tag == "Character")
        {
            other.gameObject.GetComponent<CharacterHealth>().InCircle(false, _data[_currentStep].Damage);
        }
    }

    private void Notify(string info)
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiNotifications>().Notify(info);
    }
}
