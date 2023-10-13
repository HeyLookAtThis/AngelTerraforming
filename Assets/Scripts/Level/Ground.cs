using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terrain))]
public class Ground : MonoBehaviour
{
    [SerializeField] private float _heatingDuration;

    private float _indentation;
    private float _yAxisValue;

    private Terrain _terrain;

    public float StartingTemperature { get; private set; }

    public float EndingTemperature { get; private set; }

    public float CurrentTemperature { get; private set; }

    public Vector3 StartingCoordinate { get; private set; }

    public Vector3 EndingCoordinate {  get; private set; }

    private float _secondsInMinute => 60;

    private UnityAction<float> _temperatureChanged;

    public event UnityAction<float> TemperatureChanged
    {
        add => _temperatureChanged += value;
        remove => _temperatureChanged -= value;
    }

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();

        _indentation = 2.5f;
        _yAxisValue = 1;

        SetLevelBoundaries();
    }

    private void Start()
    {
        StartingTemperature = 0;
        EndingTemperature = _heatingDuration * _secondsInMinute / GetComponent<VolcanoCreator>().Count;
        CurrentTemperature = StartingTemperature;
    }

    public void AddTemperature(float temperature)
    {
        CurrentTemperature += temperature;
        _temperatureChanged?.Invoke(CurrentTemperature);
    }

    public void RemoveTemperature(float temperature)
    {
        CurrentTemperature -= temperature;
        _temperatureChanged?.Invoke(CurrentTemperature);
    }

    private void SetLevelBoundaries()
    {
        EndingCoordinate = new Vector3(_terrain.terrainData.size.x - _indentation, _yAxisValue, _terrain.terrainData.size.z - _indentation);
        StartingCoordinate = new Vector3(_indentation, _yAxisValue, _indentation);
    }
}
