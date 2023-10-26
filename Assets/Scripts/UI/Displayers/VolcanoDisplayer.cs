using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VolcanoDisplayer : MonoBehaviour
{
    [SerializeField] private VolcanoCreator _creator;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private int _currentValue;

    private UnityAction _fulled;

    public event UnityAction Fulled
    {
        add => _fulled += value;
        remove => _fulled -= value;
    }

    public void Initialize()
    {
        _currentValue = 0;

        foreach (var volcano in _creator.Objects)
        {
            volcano.Frozed += AddValue;
            volcano.Frozed += ShowValue;
        }

        ShowValue();
    }

    public void Unsubscribe()
    {
        foreach (var volcano in _creator.Objects)
        {
            volcano.Frozed -= AddValue;
            volcano.Frozed -= ShowValue;
        }
    }

    private void ShowValue()
    {
        _textMeshProUGUI.text = $"{_currentValue}/{_creator.ObjectsCount}";
    }

    private void AddValue()
    {
        _currentValue++;

        if(_currentValue == _creator.ObjectsCount )
            _fulled?.Invoke();
    }
}
