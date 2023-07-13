using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _layerMask;

    private CharacterController _controller;
    private Player _player;

    private Vector3 _velosity;
    private Vector3 _direction;
    private bool _grounded;

    private float _circleRadius = 0.01f;
    private float _gravityValue = -9.81f;
    private float _noGravityValue = 0;
    private float _currentGravityValue;

    public float Speed => _speed;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
        TurnOffGravity();
    }

    private void OnEnable()
    {
        _player.FoundWater += OnJumpOnCloud;
    }

    private void OnDisable()
    {
        _player.FoundWater -= OnJumpOnCloud;
    }

    private void Update()
    {
        Move();

        Rotate();
    }

    private void FixedUpdate()
    {
        if (_currentGravityValue == _gravityValue)
            UzeGravity();
    }

    public void TurnOnGravity()
    {
        _currentGravityValue = _gravityValue;
    }

    private void OnJumpOnCloud()
    {
        TakeJump();
        TurnOffGravity();
    }

    private void TurnOffGravity()
    {
        _currentGravityValue = _noGravityValue;
    }

    private void UzeGravity()
    {
        _grounded = Physics.CheckSphere(transform.position, _circleRadius, _layerMask);

        if (_grounded && _velosity.y < 0)
            _velosity.y = 0;

        _velosity.y += _currentGravityValue * Time.deltaTime;
        _controller.Move(_velosity * Time.deltaTime);
    }

    private void TakeJump()
    {
        if (_grounded)
            _velosity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravityValue);
    }

    private void Move()
    {
        _direction = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));

        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_direction != Vector3.zero)
            transform.forward = _direction;
    }    
}
