using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terrain))]
public class Ground : MonoBehaviour
{
    [SerializeField] private float _heatingDuration;

    public float StartingTemperature { get; private set; }

    public float EndingTemperature { get; private set; }

    public float CurrentTemperature { get; private set; }

    private float _secondsInMinute => 60;

    private UnityAction<float> _temperatureChanged;

    public event UnityAction<float> TemperatureChanged
    {
        add => _temperatureChanged += value;
        remove => _temperatureChanged -= value;
    }

    private void Awake()
    {
        StartingTemperature = 0;
        EndingTemperature = _heatingDuration * _secondsInMinute * GetComponent<VolcanoCreator>().Count;
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
