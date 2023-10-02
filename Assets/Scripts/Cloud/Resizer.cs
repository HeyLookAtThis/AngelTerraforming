using UnityEngine;

public class Resizer : IndicatorChanger
{
    [SerializeField] private GameObject _cloudModel;

    private float _nextValue;

    private void Awake()
    {
        upperValue = 0.06f;
        lowerValue = 0.02f;
    }

    private void Update()
    {
        if(_nextValue != CurrentValue)
        {
            _cloudModel.transform.localScale = new Vector3(CurrentValue, CurrentValue, CurrentValue);
            _nextValue = CurrentValue;
        }
    }

    protected override void DecreaseCurrentValue()
    {
        if (CurrentValue > lowerValue)
            base.DecreaseCurrentValue();
    }
}
