using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterReservoir))]
public class Resizer : MonoBehaviour
{
    [SerializeField] private GameObject _cloud;

    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;

    private float _currentSize;
    private float _sizeChangerStep;

    private Coroutine _sizeChanger;

    public float CurrentSize => _currentSize;

    private void Start()
    {
        _sizeChangerStep = (_fullSize - _emptySize) / GetComponent<WaterReservoir>().Capacity;

        SetSize(_fullSize);
    }

    public void IncreaseSize()
    {
        if (_currentSize < _fullSize)
            BeginChangeSize(_fullSize);
    }

    public void ReduceSize()
    {
        if (_currentSize > _emptySize)
            BeginChangeSize(_emptySize);
    }
    public void StopChangeSize()
    {
        if (_sizeChanger != null)
            StopCoroutine(_sizeChanger);
    }

    private void BeginChangeSize(float targetSize)
    {
        StopChangeSize();
        _sizeChanger = StartCoroutine(SizeChanger(targetSize));
    }

    private IEnumerator SizeChanger(float targetSize)
    {
        var waitTime = new WaitForEndOfFrame();

        while (_currentSize != targetSize)
        {
            _currentSize = Mathf.MoveTowards(_currentSize, targetSize, _sizeChangerStep * Time.deltaTime);
            SetSize(_currentSize);
            yield return waitTime;
        }

        if (_currentSize == targetSize)
            yield break;
    }

    private void SetSize(float size)
    {
        _currentSize = size;

        _cloud.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);
    }
}
