using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private Water _water;
    [SerializeField] private Grid _grid;
    [SerializeField] private Scanner _painter;

    private Slider _slider;
    private float _valueStep;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _ground.GetArea() - _water.GetArea();
        _slider.value = 0;

        _valueStep = _grid.cellSize.x * _grid.cellSize.y;
    }

    private void OnEnable()
    {
        //_painter.FieldFilled += IncreaseValue;
    }

    private void OnDisable()
    {
        //_painter.FieldFilled -= IncreaseValue;
    }

    private void IncreaseValue()
    {
        _slider.value += _valueStep;
    }
}
