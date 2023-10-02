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
        _scanner.FoundWater += BeginAddDivisionValue;
        _scanner.WaterIsOver += StopAddDivisionValue;
        _scanner.FoundDryPlant += DecreaseCurrentValue;
    }

    private void OnDisable()
    {
        _scanner.FoundWater -= BeginAddDivisionValue;
        _scanner.WaterIsOver -= StopAddDivisionValue;
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
    }

    private void IncreaseCurrentValue()
    {
        if (_currentValue < upperValue)
            _currentValue += _divisionValue;
    }

    private float GetDivisionsNumber()
    {
        float levelCoefficient = 100;

        return GetComponent<Cloud>().Level * levelCoefficient;
    }

    private void StopAddDivisionValue()
    {
        if (_divisionValueAdder != null)
            StopCoroutine(_divisionValueAdder);
    }

    private void BeginAddDivisionValue()
    {
        StopAddDivisionValue();
        _divisionValueAdder = StartCoroutine(DivisionValueAdder());
    }

    private IEnumerator DivisionValueAdder()
    {
        float seconds = 0.01f;
        var waitTime = new WaitForSecondsRealtime(seconds);
        bool isContinue = true;

        while (isContinue)
        {
            IncreaseCurrentValue();
            yield return waitTime;
        }

        if (_currentValue >= upperValue)
            yield break;
    }
}
