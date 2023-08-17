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
    private UnityAction<bool> _foundEmptyGround;

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

    public event UnityAction<bool> FoundEmptyGround
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
        else
            TryCallGroundEvent(hit, _tilemapPlaceholder.IsFieldOccupied(hit.point));

        _previousHitCollider = hit.collider;
    }

    private void TryCallWaterEvent(RaycastHit hit)
    {
        if (hit.collider != _previousHitCollider)
        {
            Debug.Log("Water");
            _foundWater?.Invoke();        
        }
    }

    private void TryCallGroundEvent(RaycastHit hit, bool haveGroundGrass)
    {
        if (haveGroundGrass == false && GetComponent<CloudReservoir>().IsEmpty == false)
        {
            Debug.Log("EmptyGround");
            _foundEmptyGround?.Invoke(HaveFieldGrass);
        }

        if (haveGroundGrass)
        {
            Debug.Log("Grass");
            _foundGrass?.Invoke();
        }
    }
}
