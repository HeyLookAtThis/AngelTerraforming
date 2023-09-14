using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class WaterReservoir : MonoBehaviour
{
    [SerializeField] private float _capacity;

    private float _currentWaterReserve;
    private bool _isEmpty;

    private Coroutine _waterReserveChanger;
    private UnityAction _waterIsOver;

    public float Capacity => _capacity;

    public float EmptyReservoir => 0;

    public bool IsEmpty => _isEmpty;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }    

    private void Start()
    {
        _currentWaterReserve = Capacity;
    }

    public void Replenish()
    {
        if (_currentWaterReserve < Capacity)
        {
            BeginChangeWaterReserveValue(Capacity);
            _isEmpty = false;
        }
    }

    public void MakeRain()
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
    public void StopChangeWaterValue()
    {
        if (_waterReserveChanger != null)
            StopCoroutine(_waterReserveChanger);
    }

    private void BeginChangeWaterReserveValue(float targetWaterReserve)
    {
        StopChangeWaterValue();
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
