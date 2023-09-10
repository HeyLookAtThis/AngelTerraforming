using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class WaterReservoir : MonoBehaviour
{
    private float _currentWaterReserve;
    private bool _isEmpty;

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

    private void Start()
    {
        _currentWaterReserve = FullReservoir;
    }

    public void OnReplenishReservoir()
    {
        if (_currentWaterReserve < FullReservoir)
        {
            BeginChangeWaterReserveValue(FullReservoir);
            _isEmpty = false;
        }
    }

    public void OnMakeRain()
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
    public void OnStopChangeWaterValue()
    {
        if (_waterReserveChanger != null)
            StopCoroutine(_waterReserveChanger);
    }

    private void BeginChangeWaterReserveValue(float targetWaterReserve)
    {
        OnStopChangeWaterValue();
        _waterReserveChanger = StartCoroutine(WaterReserveChanger(targetWaterReserve));
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
