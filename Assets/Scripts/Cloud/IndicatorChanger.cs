using UnityEngine;

[RequireComponent(typeof(Cloud), typeof(Scanner))]
public abstract class IndicatorChanger : MonoBehaviour
{
    private Cloud _cloud;
    private Scanner _scanner;

    private float _lowerValue;
    private float _upperValue;

    private float _currentValue;
    private float _divisionValue;
    private float _divisionsNumber;

    public float LowerValue => _lowerValue;

    public float CurrentValue => _currentValue;

    public Cloud Cloud => _cloud;

    public Scanner Scanner => _scanner;

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _cloud = GetComponent<Cloud>();
    }

    private void OnEnable()
    {
        _scanner.FoundWater += IncreaseCurrentValue;
        _scanner.FoundDryPlant += DecreaseCurrentValue;
    }

    private void OnDisable()
    {
        _scanner.FoundWater -= IncreaseCurrentValue;
        _scanner.FoundDryPlant -= DecreaseCurrentValue;
    }

    protected virtual void DecreaseCurrentValue()
    {
        _currentValue -= _divisionValue;

        if(_currentValue < _lowerValue)
            _currentValue = _lowerValue;
    }

    protected void InitializeValues(float upperValueNumber, float lowerValueNumber)
    {
        _upperValue = upperValueNumber;
        _lowerValue = lowerValueNumber;

        _divisionsNumber = GetDivisionsNumber();
        _divisionValue = (_upperValue - _lowerValue) / _divisionsNumber;
        _currentValue = _upperValue;
    }

    private void IncreaseCurrentValue()
    {
        if (_currentValue < _upperValue)
            _currentValue += _divisionValue;

        if (_currentValue > _upperValue)
            _currentValue = _upperValue;
    }

    private float GetDivisionsNumber()
    {
        float levelCoefficient = 100;

        return _cloud.Level * levelCoefficient;
    }
}
