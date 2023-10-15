using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class Reservoir : IndicatorChanger
{
    private UnityAction _waterIsOver;
    private UnityAction _filledUp;
    private UnityAction<float> _increasedValue;
    private UnityAction<float> _decreasedValue;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }

    public event UnityAction FilledUp
    {
        add => _filledUp += value;
        remove => _filledUp -= value;
    }

    public event UnityAction<float> IncreasedValue 
    {
        add => _increasedValue += value;
        remove => _increasedValue -= value;
    }

    public event UnityAction<float> DecreasedValue
    {
        add => _decreasedValue += value;
        remove => _decreasedValue -= value;
    }

    public bool HaveWater => CurrentValue > LowerValue;

    private void Start()
    {
        float upperValue = Cloud.Level;
        float lowerValue = 0f;

        InitializeValues(upperValue, lowerValue);
    }

    protected override void DecreaseCurrentValue()
    {
        if (HaveWater)
        {
            base.DecreaseCurrentValue();
            _decreasedValue?.Invoke(DivisionsNumber);
        }
        else
        {
            if (Scanner.IsContainsWater() == false)
                _waterIsOver?.Invoke();
        }
    }

    protected override void IncreaseCurrentValue()
    {
        base.IncreaseCurrentValue();
        _increasedValue?.Invoke(DivisionsNumber);

        if (CurrentValue >= UpperValue)
            _filledUp?.Invoke();
    }
}
