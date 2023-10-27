using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController), typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _waterTransform;
    [SerializeField] private StartGameButton _startButton;

    private PlayerColliderController _playerCollider;
    private CharacterController _controller;
    private Coroutine _jumper;

    private Vector3 _velocity;
    private Vector3 _direction;

    private Vector3 _startPosition;

    private float _jumpHeight;
    private float _gravityValue;
    private float _noGravityValue;
    private float _currentGravityValue;

    private UnityAction<float> _running;
    private UnityAction _falling;
    private UnityAction _sitting;

    public float Speed => _speed;

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

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerCollider = GetComponent<PlayerColliderController>();

        _gravityValue = -9.81f;
        _noGravityValue = 0;
        _jumpHeight = 4;

        _startPosition = new Vector3(_waterTransform.position.x, _jumpHeight, _waterTransform.position.z);
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

        if (_currentGravityValue == _gravityValue)
            UzeGravity();
    }

    public void TurnOnGravity()
    {
        _currentGravityValue = _gravityValue;
        _falling?.Invoke();
    }

    public void SetStartingPosition()
    {
        TurnOffGravity();

        transform.position = _startPosition;
    }

    private void TurnOffGravity()
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
