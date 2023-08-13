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

    public bool HasFieldGrass {get; private set;}

    private UnityAction _foundWater;
    private UnityAction<bool> _foundEmptyGround;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction<bool> FoundEmptyGround
    {
        add => _foundEmptyGround += value;
        remove => _foundEmptyGround -= value;
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        if(TryGetNextCollider(hit) != null)
        {
            TryCallEvents(hit);
            _previousHitCollider = hit.collider;
        }
    }

    private Collider TryGetNextCollider(RaycastHit hit)
    {
        if (hit.collider != _previousHitCollider)
            return hit.collider;

        return null;
    }

    private void TryCallEvents(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            _foundWater?.Invoke();

        if (hit.collider.TryGetComponent<Ground>(out Ground ground) && GetComponent<CloudReservoir>().IsEmpty != true)
        {
            HasFieldGrass = _tilemapPlaceholder.IsFieldOccupied(hit.point);
            Debug.Log(HasFieldGrass);
            _foundEmptyGround?.Invoke(HasFieldGrass);
        }
    }
}
