using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terrain))]
public class Ground : MonoBehaviour
{
    [SerializeField] private Water _water;
    [SerializeField] private LevelGenerator _levelGenerator;

    private UnityAction<float> _temperatureChanged;

    public event UnityAction<float> TemperatureChanged
    {
        add => _temperatureChanged += value;
        remove => _temperatureChanged -= value;
    }

    public float StartingTemperature { get; private set; }

    public float EndingTemperature { get; private set; }

    public float CurrentTemperature { get; private set; }

    private float _secondsInMinute => 60;

    public LevelGenerator LevelGenerator => _levelGenerator;

    public Water Water => _water;

    public void Initialize()
    {
        StartingTemperature = 0;
        EndingTemperature = _secondsInMinute * _levelGenerator.TimeForOneVolcano * _levelGenerator.VolcanoCount;
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
}
