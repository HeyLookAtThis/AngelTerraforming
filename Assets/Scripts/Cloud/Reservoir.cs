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

    public bool HaveWater => CurrentValue > lowerValue ? true : false;

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();

        upperValue = _cloud.Level;
        lowerValue = 0f;
    }

    protected override void DecreaseCurrentValue()
    {
        if (HaveWater)
            base.DecreaseCurrentValue();
        else
            _waterIsOver?.Invoke();
    }
}
