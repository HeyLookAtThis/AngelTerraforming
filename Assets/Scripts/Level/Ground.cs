using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terrain), typeof(VolcanoCreator))]
public class Ground : MonoBehaviour
{
    [SerializeField] private Water _water;
    [SerializeField] private LevelCounter _levelGenerator;
    [SerializeField] private Thermometer _thermometer;

    public float StartingTemperature { get; private set; }

    public float EndingTemperature { get; private set; }

    public float CurrentTemperature { get; private set; }

    private float _secondsInMinute => 60;

    public LevelCounter LevelGenerator => _levelGenerator;

    public Water Water => _water;

    private void OnEnable()
    {
        _levelGenerator.StartGameButton.AddAction(Initialize);
    }

    private void OnDisable()
    {
        _levelGenerator.StartGameButton.RemoveAction(Initialize);
    }

    private void Initialize()
    {
        StartingTemperature = 0;
        EndingTemperature = _secondsInMinute * _levelGenerator.TimeForOneVolcano * _levelGenerator.CurrentLevel;
        CurrentTemperature = StartingTemperature;
        _thermometer.Initialize(StartingTemperature, EndingTemperature);
    }

    public void AddTemperature(float temperature)
    {
        CurrentTemperature += temperature;
        _thermometer.BeginChangeValue(CurrentTemperature);
    }
}
