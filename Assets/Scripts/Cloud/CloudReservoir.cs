using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class CloudReservoir : MonoBehaviour
{
    private float _currentWaterReserve;

    private Cloud _cloud;

    public Player Player { get; private set; }

    public float FullReservoir { get; } = 5;

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
    }

    private void OnEnable()
    {
        _cloud.FoundGround += OnMakeRain;
        _cloud.FoundWater += OnReplenishReservoir;
    }

    private void OnDisable()
    {
        _cloud.FoundGround -= OnMakeRain;
        _cloud.FoundWater -= OnReplenishReservoir;
    }

    private void Start()
    {
        _currentWaterReserve = FullReservoir;
    }

    private void OnReplenishReservoir()
    {
        if (_currentWaterReserve < FullReservoir)
        {
            _currentWaterReserve += Time.deltaTime;
            _cloud.IncreaseSize();
        }
    }

    private void OnMakeRain()
    {
        if (_currentWaterReserve > EmptyReservoir)
        {
            _currentWaterReserve -= Time.deltaTime;
            _cloud.ReduceSize();
        }
        else
        {
            _waterIsOver?.Invoke();
        }
    }
}
