using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class Reservoir : IndicatorChanger
{
    private Cloud _cloud;
    private UnityAction _waterIsOver;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }    

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
        upperValue = _cloud.Level;
        lowerValue = 0f;
    }

    protected override void DecreaseCurrentValue()
    {
        if (CurrentValue > lowerValue)
            base.DecreaseCurrentValue();
        else
            if (_cloud.IsAboveWater == false)
                _waterIsOver?.Invoke();
    }
}
