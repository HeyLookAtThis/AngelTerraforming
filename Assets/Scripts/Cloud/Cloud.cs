using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private float _level;
    [SerializeField] private PlayerColliderController _playerCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _targetPlace;

    public PlayerColliderController PlayerCollider => _playerCollider;

    public PlayerMovement PlayerMovement => _playerMovement;

    public Transform TargetPlace => _targetPlace;

    public float Level => _level;
}
