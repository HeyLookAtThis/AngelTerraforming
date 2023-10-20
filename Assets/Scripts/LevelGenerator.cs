using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] float _timeForOneVolcano;
    [SerializeField] private VolcanoCreator _volcanoCreator;
    [SerializeField] private TreesCreator _treesCreator;
    [SerializeField] private Thermometer _thermometer;
    [SerializeField] private Ground _ground;

    private int _firstLevel;
    private int _lastLevel;
    [SerializeField] private int _currentLevel;
    private int _nextLevel;

    public int CurrentLevel => _currentLevel;

    public float TimeForOneVolcano => _timeForOneVolcano;

    public float VolcanoCount => _volcanoCreator.VolcanoCount;

    private void Start()
    {
        _firstLevel = 1;
        _lastLevel = 5;
        SetLevel(_currentLevel);
    }

    public void SetNextLevel()
    {
        _nextLevel++;
        SetLevel(_nextLevel);
    }

    private void SetLevel(int nextLevel)
    {
        ClearTerrain();

        if (nextLevel <= _lastLevel)
        {
            _currentLevel = nextLevel;
            GenerateLevel();
        }
    }

    private void GenerateLevel()
    {
        _ground.Initialize();
        _volcanoCreator.Create();
        _treesCreator.Create();
        _thermometer.Initialize();
    }

    private void ClearTerrain()
    {
        _volcanoCreator.ClearLevel();
        _treesCreator.ClearLevel();
    }
}
