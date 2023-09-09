using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class CloudReservoir : MonoBehaviour
{
    private float _currentWaterReserve;
    private bool _isEmpty;

    private Cloud _cloud;
    private Coroutine _waterReserveChanger;

    private UnityAction _waterIsOver;

    public float FullReservoir { get; } = 3;

    public float EmptyReservoir { get; } = 0;

    public bool IsEmpty => _isEmpty;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
    }

    private void OnEnable()
    {
        //_cloud.FoundEmptyGround += OnMakeRain;
        _cloud.FoundWater += OnReplenishReservoir;
        _cloud.FoundGrass += StopChangeWaterReserveValue;
    }

    private void OnDisable()
    {
        //_cloud.FoundEmptyGround -= OnMakeRain;
        _cloud.FoundWater -= OnReplenishReservoir;
        _cloud.FoundGrass -= StopChangeWaterReserveValue;
    }

    private void Start()
    {
        _currentWaterReserve = FullReservoir;
    }

    private void OnReplenishReservoir()
    {
        if (_currentWaterReserve < FullReservoir)
        {
            BeginChangeWaterReserveValue(FullReservoir);
            _isEmpty = false;
        }
    }

    private void OnMakeRain()
    {
        if (_currentWaterReserve > EmptyReservoir)
        {
            BeginChangeWaterReserveValue(EmptyReservoir);
        }
        else
        {
            _isEmpty = true;
            _waterIsOver?.Invoke();
        }
    }

    private void BeginChangeWaterReserveValue(float targetWaterReserve)
    {
        StopChangeWaterReserveValue();
        _waterReserveChanger = StartCoroutine(WaterReserveChanger(targetWaterReserve));
    }

    private void StopChangeWaterReserveValue()
    {
        if (_waterReserveChanger != null)
            StopCoroutine(_waterReserveChanger);
    }

    private IEnumerator WaterReserveChanger(float targetWaterReserve)
    {
        var waitTime = new WaitForEndOfFrame();

        while (_currentWaterReserve != targetWaterReserve)
        {
            _currentWaterReserve = Mathf.MoveTowards(_currentWaterReserve, targetWaterReserve, Time.deltaTime);
            yield return waitTime;
        }

        if (_currentWaterReserve == targetWaterReserve)
            yield break;
    }
}
