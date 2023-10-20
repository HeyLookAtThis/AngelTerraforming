using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    [SerializeField] private Ground _ground;

    private Coroutine _valueChanger;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _ground.TemperatureChanged += BeginChangeValue;
    }

    private void OnDisable()
    {
        _ground.TemperatureChanged -= BeginChangeValue;
    }

    public void Initialize()
    {
        _slider.minValue = _ground.StartingTemperature;
        _slider.maxValue = _ground.EndingTemperature;
        _slider.value = _ground.CurrentTemperature;
    }

    private void BeginChangeValue(float currentValue)
    {
        if (_valueChanger != null)
            StopCoroutine(_valueChanger);

        _valueChanger = StartCoroutine(ValueChanger(currentValue));
    }

    private IEnumerator ValueChanger(float currentValue)
    {
        float seconds = 0.01f;
        float changeScale = 0.2f;
        var waitTime = new WaitForSeconds(seconds);

        while (_slider.value != currentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, currentValue, changeScale);
            yield return waitTime;
        }

        if (_slider.value == currentValue)
            yield break;
    }
}
