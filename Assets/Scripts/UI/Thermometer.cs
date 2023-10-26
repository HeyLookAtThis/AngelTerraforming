using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    private Coroutine _valueChanger;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Initialize(float startingTemperature, float endingTemperature)
    {
        _slider.minValue = startingTemperature;
        _slider.maxValue = endingTemperature;
        _slider.value = startingTemperature;
    }

    public void BeginChangeValue(float currentValue)
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
