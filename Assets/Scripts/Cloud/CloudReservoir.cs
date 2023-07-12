using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class CloudReservoir : MonoBehaviour
{
    private float _currentWaterReserve;
    private float _targetWaterReserve;

    private Coroutine _waterReserveChanger;

    public Player Player { get; private set; }

    public float FullReservoir { get; } = 100;

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
        Player = GetComponent<Cloud>().Player;
        _currentWaterReserve = FullReservoir;
    }

    private void OnEnable()
    {
        Player.FoundWater += BeginChangeWaterValue;
    }

    private void OnDisable()
    {
        Player.FoundWater -= BeginChangeWaterValue;
    }

    private void Update()
    {
        Debug.Log(_currentWaterReserve);
    }

    private void BeginChangeWaterValue()
    {
        float changerStep = 0;
        float targetWaterReserve = _currentWaterReserve;

        //if(collider.TryGetComponent<Water>(out Water water))
        //{
        //    changerStep = 1;
        //    targetWaterReserve = FullReservoir;
        //}
        //else if (collider.TryGetComponent<Ground>(out Ground ground))
        //{
        //    changerStep = -1;
        //    targetWaterReserve = EmptyReservoir;
        //}

        if (_waterReserveChanger != null)
            StopCoroutine(_waterReserveChanger);

        StartCoroutine(WaterReserveChanger(changerStep, targetWaterReserve));
    }

    private IEnumerator WaterReserveChanger(float index, float targetWaterReserve)
    {
        float seconds = 0.2f;
        var waitTime = new WaitForSeconds(seconds);

        while(_currentWaterReserve != targetWaterReserve)
        {
            _currentWaterReserve += index;

            if (_currentWaterReserve == EmptyReservoir)
                _waterIsOver?.Invoke();

            yield return waitTime;
        }

        if (_currentWaterReserve == targetWaterReserve)
            yield break;
    }
}
