using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Reservoir))]
public class Resizer : MonoBehaviour
{
    [SerializeField] private GameObject _cloud;

    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;

    private float _currentSize;
    private float _sizeChangerStep;

    public float CurrentSize => _currentSize;

    private void Start()
    {
        _sizeChangerStep = (_fullSize - _emptySize) / GetComponent<Reservoir>().ChangeStepsCount;

        _currentSize = _fullSize;
        SetSize();
    }

    public void IncreaseSize()
    {
        if (_currentSize < _fullSize)
            _currentSize += _sizeChangerStep;

        SetSize();
    }

    public void DecreaseSize()
    {
        if (_currentSize > _emptySize)
            _currentSize -= _sizeChangerStep;

        SetSize();
    }

    private void SetSize()
    {
        _cloud.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);
    }
}
