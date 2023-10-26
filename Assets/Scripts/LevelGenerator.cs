using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] float _timeForOneVolcano;
    [SerializeField] private VolcanoCreator _volcanoCreator;
    [SerializeField] private TreesCreator _treesCreator;
    [SerializeField] private CoinsCreator _coinCreator;
    [SerializeField] private Thermometer _thermometer;
    [SerializeField] private Ground _ground;
    [SerializeField] private VolcanoDisplayer _displayer;

    private int _firstLevel;
    private int _lastLevel;
    private int _currentLevel;
    private int _nextLevel;

    public int CurrentLevel => _currentLevel;

    public float TimeForOneVolcano => _timeForOneVolcano;

    public float VolcanoCount => _volcanoCreator.ObjectsCount;

    private void Awake()
    {
        _firstLevel = 1;
        _lastLevel = 5;
        SetLevel(_firstLevel);
    }

    public void GenerateLevel()
    {
        _volcanoCreator.Create();
        _ground.Initialize();
        _treesCreator.Create();
        _thermometer.Initialize();
        _displayer.Initialize();

        if(_coinCreator.Created == false)
        {
            _coinCreator.Create();
        }
    }

    public void SetNextLevel()
    {
        _nextLevel += _currentLevel;
        SetLevel(_nextLevel);
    }

    private void SetLevel(int level)
    {
        ClearTerrain();

        if (level <= _lastLevel)
            _currentLevel = level;
    }

    private void ClearTerrain()
    {
        _volcanoCreator.ClearLevel();
        _treesCreator.ClearLevel();
        _displayer.Unsubscribe();
    }
}
