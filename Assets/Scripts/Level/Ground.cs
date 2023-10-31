using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Terrain), typeof(VolcanoCreator))]
public class Ground : MonoBehaviour
{
    [SerializeField] private Water _water;
    [SerializeField] private LevelCounter _levelCounter;
    [SerializeField] private Thermometer _thermometer;
    [SerializeField] private LevelStarter _levelStarter;
    [SerializeField] private LevelFinisher _levelFinisher;

    public float StartingTemperature { get; private set; }

    public float EndingTemperature { get; private set; }

    public float CurrentTemperature { get; private set; }

    private float _secondsInMinute => 60;

    public LevelCounter LevelGenerator => _levelCounter;

    public LevelStarter LevelStarter => _levelStarter;

    public LevelFinisher LevelFinisher => _levelFinisher;

    public Water Water => _water;

    private void OnEnable()
    {
        _levelStarter.Beginning += Initialize;
    }

    private void OnDisable()
    {
        _levelStarter.Beginning -= Initialize;
    }

    private void Initialize()
    {
        StartingTemperature = 0;
        EndingTemperature = _secondsInMinute * _levelCounter.TimeForOneVolcano * _levelCounter.CurrentLevel;
        CurrentTemperature = StartingTemperature;
        _thermometer.Initialize(StartingTemperature, EndingTemperature);
    }

    public void AddTemperature(float temperature)
    {
        CurrentTemperature += temperature;
        _thermometer.BeginChangeValue(CurrentTemperature);
    }
}
