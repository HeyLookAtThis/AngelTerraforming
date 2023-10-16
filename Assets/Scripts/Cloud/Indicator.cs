using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud), typeof(Scanner))]
public abstract class Indicator : MonoBehaviour
{
    private Cloud _cloud;
    private Scanner _scanner;
    private Coroutine _valueChanger;

    private float _lowerValue;
    private float _upperValue;

    private float _currentValue;
    private float _divisionValue;
    private float _divisionsNumber;

    private UnityAction _changedValue;

    public event UnityAction ChangedValue
    {
        add => _changedValue += value;
        remove => _changedValue -= value;
    }

    public float LowerValue => _lowerValue;

    public float CurrentValue => _currentValue;

    public float UpperValue => _upperValue;

    public float DivisionsNumber => _divisionsNumber;

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
        BeginChangeValue(_currentValue - _divisionValue);

        if(_currentValue < _lowerValue)
            _currentValue = _lowerValue;
    }

    protected virtual void IncreaseCurrentValue()
    {
        if (_currentValue < _upperValue)
            BeginChangeValue(_currentValue + _divisionValue);

        if (_currentValue > _upperValue)
            _currentValue = _upperValue;
    }

    protected void InitializeValues(float upperValueNumber, float lowerValueNumber)
    {
        _upperValue = upperValueNumber;
        _lowerValue = lowerValueNumber;

        _divisionsNumber = GetDivisionsNumber();
        _divisionValue = (_upperValue - _lowerValue) / _divisionsNumber;
        _currentValue = _upperValue;
    }

    private float GetDivisionsNumber()
    {
        float levelCoefficient = 100;

        return _cloud.Level * levelCoefficient;
    }

    private void BeginChangeValue(float targetValue)
    {
        if(_valueChanger != null)
            StopCoroutine(_valueChanger);

        _valueChanger = StartCoroutine(ValueChanger(targetValue));
    }

    private IEnumerator ValueChanger(float targetValue)
    {
        float seconds = 0.2f;

        var waitTime = new WaitForSeconds(seconds);

        while(CurrentValue != targetValue)
        {
            _currentValue = Mathf.MoveTowards(_currentValue, targetValue, _divisionValue);
            _changedValue?.Invoke();
            yield return waitTime;
        }

        if (CurrentValue == targetValue)
            yield break;
    }
}
