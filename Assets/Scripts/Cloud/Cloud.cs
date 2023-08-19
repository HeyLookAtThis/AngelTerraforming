using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CloudReservoir))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TilemapPainter _tilemapPlaceholder;

    private Collider _previousHitCollider;

    public Player Player => _player;

    public PlayerMovement PlayerMovement => _playerMovement;

    public bool HaveFieldGrass {get; private set;}

    private UnityAction _foundWater;
    private UnityAction _foundGrass;
    private UnityAction _foundEmptyGround;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction FoundGrass
    {
        add=>_foundGrass += value;
        remove => _foundGrass -= value;
    }

    public event UnityAction FoundEmptyGround
    {
        add => _foundEmptyGround += value;
        remove => _foundEmptyGround -= value;
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        TryCallEvents(hit);
    }
    private void TryCallEvents(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            TryCallWaterEvent(hit);

        if ((hit.collider.TryGetComponent<Ground>(out Ground ground)))
            TryCallGroundEvent(_tilemapPlaceholder.IsFieldOccupied(hit.point));

        _previousHitCollider = hit.collider;
    }

    private void TryCallWaterEvent(RaycastHit hit)
    {
        if (hit.collider != _previousHitCollider)
            _foundWater?.Invoke();        
    }

    private void TryCallGroundEvent(bool haveGroundGrass)
    {
        if (haveGroundGrass == false && GetComponent<CloudReservoir>().IsEmpty == false)
            _foundEmptyGround?.Invoke();

        if (haveGroundGrass)
            _foundGrass?.Invoke();
    }
}
