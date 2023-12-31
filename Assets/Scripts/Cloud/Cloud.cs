using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaterReservoir))]
[RequireComponent(typeof(Resizer))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private PlayerColliderController _playerCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GrassPainter _tilemapPainter;

    private int _radius = 2;
    private bool _isGrassGrowDiferred = true;
    private bool _locatedUnderPlayer;

    private Collider _previousHitCollider;
    private WaterReservoir _reservoir;
    private Resizer _resizer;

    public PlayerColliderController PlayerCollider => _playerCollider;

    public PlayerMovement PlayerMovement => _playerMovement;

    public bool LocatedUnderPlayer => _locatedUnderPlayer;

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
        if (_locatedUnderPlayer == false)
            return;

        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        if (hit.collider.TryGetComponent<Ground>(out Ground ground))
        {
            if (_tilemapPainter.CanFillCell(hit.point) && _reservoir.IsEmpty == false)
            {
                _tilemapPainter.BeginFillCell(hit.point, _radius, _isGrassGrowDiferred);

                _reservoir.MakeRain();
                _resizer.ReduceSize();
            }
            else
            {
                _reservoir.StopChangeWaterValue();
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
            _reservoir.Replenish();
            _resizer.IncreaseSize();
        }

        if (hit.collider.TryGetComponent<Tree>(out Tree tree))
        {
            tree.GrowGrassAround();
        }
    }

    public void TurnOnLocationUnderPlayer()
    {
        _locatedUnderPlayer = true;
    }

    public void TurnOffLacationUnderPlayer()
    {
        _locatedUnderPlayer = false;
    }
}
