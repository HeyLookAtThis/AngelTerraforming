using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] float _timeForOneVolcano;
    [SerializeField] private LevelStarter _levelStarter;

    private int _lastLevel;
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public float TimeForOneVolcano => _timeForOneVolcano;

    private void Awake()
    {
        _lastLevel = 5;
        _currentLevel = 1;
    }

    private void OnEnable()
    {
        _levelStarter.Beginning += SetNextLevel;
    }

    private void OnDisable()
    {
        _levelStarter.Beginning -= SetNextLevel;
    }

    public void SetNextLevel()
    {
        if (_currentLevel <= _lastLevel)
            _currentLevel++;
    }
}
