using UnityEngine.Events;

public class Reservoir : IndicatorChanger
{
    private UnityAction _waterIsOver;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }    

    private void Awake()
    {
        upperValue = GetComponent<Cloud>().Level;
        lowerValue = 0f;
    }

    protected override void DecreaseCurrentValue()
    {
        if(CurrentValue > lowerValue)
            base.DecreaseCurrentValue();
        else
            _waterIsOver?.Invoke();
    }
}
