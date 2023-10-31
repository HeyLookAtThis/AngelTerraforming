using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController), typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _speed;

    private PlayerColliderController _playerCollider;
    private CharacterController _controller;
    private Coroutine _jumper;

    private Vector3 _velocity;
    private Vector3 _direction;

    private float _gravityValue;
    private float _noGravityValue;
    private float _currentGravityValue;

    private UnityAction<float> _running;
    private UnityAction _falling;
    private UnityAction _sitting;
    private UnityAction<Vector3> _changePosition;

    public event UnityAction<float> Runnibg
    {
        add => _running += value;
        remove => _running -= value;
    }

    public event UnityAction Falling
    {
        add => _falling += value;
        remove => _falling -= value;
    }

    public event UnityAction Sitting
    {
        add => _sitting += value;
        remove => _sitting -= value;
    }

    public event UnityAction<Vector3> ChangePosition
    {
        add => _changePosition += value;
        remove => _changePosition -= value;
    }

    public float Speed => _speed;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerCollider = GetComponent<PlayerColliderController>();

        _gravityValue = -9.81f;
        _noGravityValue = 0;
    }

    private void OnEnable()
    {
        _playerCollider.FoundWater += OnBeginToJump;
    }

    private void OnDisable()
    {
        _playerCollider.FoundWater -= OnBeginToJump;
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
        _falling?.Invoke();
    }

    public void TurnOffGravity()
    {
        _currentGravityValue = _noGravityValue;
        _sitting?.Invoke();
    }

    private void UzeGravity()
    {
        if (_playerCollider.IsGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
            return;
        }

        _velocity.y += _currentGravityValue * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }

    private void Move()
    {
        _direction = Vector3.zero;
        _direction.x = _joystick.Horizontal * _speed * Time.deltaTime;
        _direction.z = _joystick.Vertical * _speed * Time.deltaTime;

        if (_playerCollider.IsGrounded && _direction != Vector3.zero)
            _running?.Invoke(_speed);
        else
            _running?.Invoke(0);

        _controller.Move(_direction);
        _changePosition?.Invoke(transform.position);
    }

    private void Rotate()
    {
        if (_direction != Vector3.zero)
            transform.forward = _direction;
    }

    private void OnBeginToJump()
    {
        TurnOffGravity();

        if (_jumper != null)
            StopCoroutine(_jumper);

        _jumper = StartCoroutine(JumpTaker());
    }
    
    private IEnumerator JumpTaker()
    {
        var waitTime = new WaitForEndOfFrame();

        while (transform.position.y != _jumpHeight)
        {
            transform.DOMoveY(_jumpHeight, Speed * Time.deltaTime);
            yield return waitTime;
        }

        if (transform.position.y >= _jumpHeight)
        {
            transform.position = new Vector3(transform.position.x, _jumpHeight, transform.position.z);
            yield break;
        }
    }
}
