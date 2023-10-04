using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private float _level;
    [SerializeField] private PlayerColliderController _playerCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _targetPlace;

    private Scanner _scanner;

    public PlayerColliderController PlayerCollider => _playerCollider;

    public PlayerMovement PlayerMovement => _playerMovement;

    public Transform TargetPlace => _targetPlace;

    public float Level => _level;

    public bool IsAboveWater { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
    }

    public void TurnOnLocationUnderPlayer()
    {
        _scanner.Activate();
    }

    public void TurnOffLacationUnderPlayer()
    {
        _scanner.Deactivate();
    }

    public void TurnOnIsAboveWater()
    {
        IsAboveWater = true;
        Debug.Log(IsAboveWater);
    }

    public void TurnOffIsAboveWater()
    {
        IsAboveWater = false;
        Debug.Log(IsAboveWater);
    }
}
