using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaterReservoir))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TilemapPainter _tilemapPlaceholder;

    private int _radius = 2;
    private bool _isGrassGrowDiferred = true;

    private Collider _previousHitCollider;
    private WaterReservoir _reservoir;
    private Resizer _resizer;

    public Player Player => _player;

    public PlayerMovement PlayerMovement => _playerMovement;

    private UnityAction _foundWater;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    private void Start()
    {
        _reservoir = GetComponent<WaterReservoir>();
        _resizer = GetComponent<Resizer>();
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        if (hit.collider.TryGetComponent<Ground>(out Ground ground))
        {
            if (_tilemapPlaceholder.IsFieldOccupied(hit.point) == false && _reservoir.IsEmpty == false)
            {
                _tilemapPlaceholder.OnBeginFillCell(_radius, _isGrassGrowDiferred);
                _reservoir.OnMakeRain();
                _resizer.OnReduceSize();
            }
            else
            {
                _reservoir.OnStopChangeWaterValue();
                _resizer.StopChangeSize();
            }
        }

        if (hit.collider != _previousHitCollider)
            _previousHitCollider = hit.collider;
        else
            return;

        if (hit.collider.TryGetComponent<Water>(out Water water))
        {
            _foundWater?.Invoke();
            _reservoir.OnReplenishReservoir();
            _resizer.OnIncreaseSize();
        }

        if (hit.collider.TryGetComponent<Tree>(out Tree tree))
        {
            tree.GrowGrassAround();
        }
    }
}
