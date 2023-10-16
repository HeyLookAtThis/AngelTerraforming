using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScaleDisplayer : MonoBehaviour
{
    [SerializeField] private Reservoir _reservoir;

    private RectTransform _rectTransform;

    private Quaternion _rotation;
    private Image _image;
    private float _totalValue;
    private float _previousValue;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _reservoir.ChangedValue += ChangeValue;
    }

    private void OnDisable()
    {
        _reservoir.ChangedValue -= ChangeValue;
    }

    private void Start()
    {
        _rotation = new Quaternion(0, 90, 90, 1);
        _totalValue = 1;
        _image.fillAmount = _totalValue;
    }

    private void Update()
    {
        _rectTransform.rotation = _rotation;
    }

    private void ChangeValue()
    {
        if(_reservoir.CurrentValue > _previousValue)
        {
            _image.fillAmount += _totalValue / _reservoir.DivisionsNumber;
            _previousValue = _reservoir.CurrentValue;
        }
        else
        {
            _image.fillAmount -= _totalValue / _reservoir.DivisionsNumber;
            _previousValue = _reservoir.CurrentValue;
        }
    }
}
