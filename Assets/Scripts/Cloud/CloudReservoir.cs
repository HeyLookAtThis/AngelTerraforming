using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class CloudReservoir : MonoBehaviour
{
    private float _currentWaterReserve;
    private float _targetWaterReserve;

    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;
    private float _currentSize;
    private float _sizeOfStep;

    private Cloud _cloud;
    private Coroutine _waterReserveChanger;

    public Player Player { get; private set; }

    public float FullReservoir { get; } = 10;

    public float EmptyReservoir { get; } = 0;

    public float CurrentWaterReserve => _currentWaterReserve;

    private UnityAction _waterIsOver;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
        Player = _cloud.Player;
        _currentWaterReserve = FullReservoir;
        _sizeOfStep = (_fullSize - _emptySize) / FullReservoir;
    }

    private void OnEnable()
    {
        _cloud.FoundGround += OnFoundGround;
        _cloud.FoundGround += OnFoundWater;
    }

    private void OnDisable()
    {
        _cloud.FoundGround -= OnFoundGround;
        _cloud.FoundGround -= OnFoundWater;
    }

    private void OnFoundWater()
    {
        _currentWaterReserve += Time.deltaTime;

        if (_currentSize < _fullSize)
        {
            _currentSize += _sizeOfStep;
            transform.localScale = new Vector3(_currentSize, _emptySize, _currentSize);
        }

        //BeginChangeWaterValue(index, FullReservoir);
    }

    private void OnFoundGround()
    {
        if (_currentWaterReserve > EmptyReservoir)
        {
            _currentWaterReserve -= Time.deltaTime;

            if (_currentSize > _emptySize)
            {
                _currentSize -= _sizeOfStep;
                transform.localScale = new Vector3(_currentSize, _emptySize, _currentSize);                
            }
        }
        else
        {
            _waterIsOver?.Invoke();
        }            
    }

    private void BeginChangeWaterValue(float index, float targetWaterReserve)
    {
        if (_waterReserveChanger != null)
            StopCoroutine(_waterReserveChanger);

        StartCoroutine(WaterReserveChanger(index, targetWaterReserve));
    }

    private IEnumerator WaterReserveChanger(float index, float targetWaterReserve)
    {
        float seconds = 0.2f;
        var waitTime = new WaitForSeconds(seconds);

        while(_currentWaterReserve != targetWaterReserve)
        {
            _currentWaterReserve += index;
            _currentSize += _sizeOfStep * index;

            if (_currentWaterReserve <= EmptyReservoir)
                _waterIsOver?.Invoke();

            yield return waitTime;
        }

        if (_currentWaterReserve == targetWaterReserve)
            yield break;
    }
}
