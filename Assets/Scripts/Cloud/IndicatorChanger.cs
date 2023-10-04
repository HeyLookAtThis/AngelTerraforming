using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public abstract class IndicatorChanger : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;

    protected float lowerValue;
    protected float upperValue;

    private float _currentValue;
    private float _divisionValue;
    private float _divisionsNumber;

    private Coroutine _divisionValueAdder;

    public float CurrentValue => _currentValue;

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

    private void Start()
    {
        _divisionsNumber = GetDivisionsNumber();
        _divisionValue = (upperValue - lowerValue) / _divisionsNumber;
        _currentValue = upperValue;
    }

    protected virtual void DecreaseCurrentValue()
    {
        _currentValue -= _divisionValue;

        if(_currentValue < lowerValue)
            _currentValue = lowerValue;
    }

    private void IncreaseCurrentValue()
    {
        if (_currentValue < upperValue)
            _currentValue += _divisionValue;

        if (_currentValue > upperValue)
            _currentValue = upperValue;

    }

    private float GetDivisionsNumber()
    {
        float levelCoefficient = 100;

        return GetComponent<Cloud>().Level * levelCoefficient;
    }
}
