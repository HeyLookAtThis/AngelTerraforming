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
    private float _totalAmount;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _reservoir.IncreasedValue += AddAmount;
        _reservoir.DecreasedValue += RemoveAmount;
    }

    private void OnDisable()
    {
        _reservoir.IncreasedValue -= AddAmount;
        _reservoir.DecreasedValue -= RemoveAmount;
    }

    private void Start()
    {
        _rotation = new Quaternion(0, 90, 90, 1);
        _totalAmount = 1;
        _image.fillAmount = _totalAmount;
    }

    private void Update()
    {
        _rectTransform.rotation = _rotation;
    }

    private void AddAmount(float divisionsNumber)
    {
        _image.fillAmount += _totalAmount / divisionsNumber;
    }

    private void RemoveAmount(float divisionsNumber)
    {
        _image.fillAmount -= _totalAmount / divisionsNumber;
    }
}
