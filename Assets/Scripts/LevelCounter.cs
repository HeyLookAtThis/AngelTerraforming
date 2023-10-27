using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] float _timeForOneVolcano;
    [SerializeField] private StartGameButton _gameButton;

    private int _lastLevel;
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public float TimeForOneVolcano => _timeForOneVolcano;

    public StartGameButton StartGameButton => _gameButton;

    private void Awake()
    {
        _lastLevel = 5;
    }

    public void SetNextLevel()
    {
        _currentLevel++;
        SetLevel(_currentLevel);
    }

    private void SetLevel(int level)
    {
        if (level <= _lastLevel)
            _currentLevel = level;
    }
}
