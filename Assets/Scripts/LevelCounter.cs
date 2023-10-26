using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] float _timeForOneVolcano;
    [SerializeField] private StartGameButton _gameButton;

    private int _firstLevel;
    private int _lastLevel;
    private int _currentLevel;
    private int _nextLevel;

    public int CurrentLevel => _currentLevel;

    public float TimeForOneVolcano => _timeForOneVolcano;

    public StartGameButton StartGameButton => _gameButton;

    private void Awake()
    {
        _firstLevel = 1;
        _lastLevel = 5;
        SetLevel(_firstLevel);
    }

    private void OnEnable()
    {
        _gameButton.AddAction(SetNextLevel);
    }

    private void OnDisable()
    {
        _gameButton.RemoveAction(SetNextLevel);
    }

    private void SetNextLevel()
    {
        _nextLevel += _currentLevel;
        SetLevel(_nextLevel);
    }

    private void SetLevel(int level)
    {
        if (level <= _lastLevel)
            _currentLevel = level;
    }
}
