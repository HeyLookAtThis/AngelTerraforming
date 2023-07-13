using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CloudReservoir))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;
    private float _currentSize;
    private float _targetSize;
    private float _sizeOfStep;

    private Coroutine _sizeChanger;

    public Player Player => _player;

    public PlayerMovement PlayerMovement => _playerMovement;

    private UnityAction _foundWater;
    private UnityAction _foundGround;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction FoundGround
    {
        add => _foundGround += value;
        remove => _foundGround -= value;
    }

    private void Start()
    {
        SetSize(_fullSize);

        _sizeOfStep = (_fullSize - _emptySize) / GetComponent<CloudReservoir>().FullReservoir;
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        CheckCollider(hit);
    }

    public void IncreaseSize()
    {
        if (_currentSize < _fullSize)
        {
            _targetSize = _currentSize + _sizeOfStep;
            BeginchangeSize();
            _currentSize = _targetSize;
        }
        else
        {
            SetSize(_fullSize);
        }
    }

    public void ReduceSize()
    {
        if (_currentSize > _emptySize)
        {
            _targetSize = _currentSize - _sizeOfStep;
            BeginchangeSize();
            _currentSize = _targetSize;
        }
        else
        {
            SetSize(_emptySize);
        }
    }

    private void SetSize(float size)
    {
        transform.localScale = new Vector3(size, _emptySize, size);
    }

    private void CheckCollider(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            _foundWater?.Invoke();
        else if (hit.collider.TryGetComponent<Ground>(out Ground ground))
            _foundGround?.Invoke();
    }

    private void BeginchangeSize( )
    {
        if (_sizeChanger != null)
            StopCoroutine(_sizeChanger);

        _sizeChanger = StartCoroutine(SizeChanger());
    }

    private IEnumerator SizeChanger( )
    {
        float seconds = 1f;
        var waitTime = new WaitForSeconds(seconds);

        while (_currentSize != _targetSize)
        {
            _currentSize = Mathf.MoveTowards(_currentSize, _targetSize, seconds);
            SetSize(_currentSize);
            yield return waitTime;
        }

        if (_currentSize == _targetSize)
            yield break;

    }
}
