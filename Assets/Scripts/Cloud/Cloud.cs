using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CloudReservoir))]
[RequireComponent(typeof(MeshCollider))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _fullSize = 0.06f;
    private float _emptySize = 0.02f;

    private float _currentSize;
    private float _sizeChangerStep;

    private Coroutine _sizeChanger;
    private MeshCollider _cloudCollider;
    private Collider _previousHitCollider;

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
        _cloudCollider = GetComponent<MeshCollider>();

        _sizeChangerStep = (_fullSize - _emptySize) / GetComponent<CloudReservoir>().FullReservoir;
        SetSize(_fullSize);
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        CheckCollider(hit);
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

    private void CheckCollider(RaycastHit hit)
    {
        if (hit.collider != _previousHitCollider)
        {
            TryCallEvent(hit);
            _previousHitCollider = hit.collider;
        }
    }

    private void TryCallEvent(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            _foundWater?.Invoke();
        else if (hit.collider.TryGetComponent<Ground>(out Ground ground))
            _foundGround?.Invoke();
    }

    private void BeginChangeSize(float targetSize)
    {
        if (_sizeChanger != null)
            StopCoroutine(_sizeChanger);

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
