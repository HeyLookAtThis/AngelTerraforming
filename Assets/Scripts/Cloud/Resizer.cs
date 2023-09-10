using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterReservoir))]
public class Resizer : MonoBehaviour
{
    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;

    private float _currentSize;
    private float _sizeChangerStep;

    private Coroutine _sizeChanger;

    private void Start()
    {
        _sizeChangerStep = (_fullSize - _emptySize) / GetComponent<WaterReservoir>().FullReservoir;

        SetSize(_fullSize);
    }

    public void OnIncreaseSize()
    {
        if (_currentSize < _fullSize)
            BeginChangeSize(_fullSize);
    }

    public void OnReduceSize()
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

        transform.localScale = new Vector3(_currentSize, _emptySize, _currentSize);
    }
}
