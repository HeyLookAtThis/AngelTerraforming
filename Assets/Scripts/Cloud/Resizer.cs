using UnityEngine;

public class Resizer : IndicatorChanger
{
    [SerializeField] private GameObject _cloudModel;

    private float _nextValue;

    private void Start()
    {
        float upperValue = 0.06f;
        float lowerValue = 0.02f;

        InitializeValues(upperValue, lowerValue);
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
        if (CurrentValue > LowerValue)
            base.DecreaseCurrentValue();
    }
}
