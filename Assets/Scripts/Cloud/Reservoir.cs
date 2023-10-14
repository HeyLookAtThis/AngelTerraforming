using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class Reservoir : IndicatorChanger
{
    private UnityAction _waterIsOver;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
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
        }
        else
        {
            if (Scanner.IsContainsWater() == false)
                _waterIsOver?.Invoke();
        }
    }
}
