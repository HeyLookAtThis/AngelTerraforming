using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;

    private PlayerColliderController _playerCollider;
    private CharacterController _controller;
    private Coroutine _jumper;

    private Vector3 _velosity;
    private Vector3 _direction;

    private float _gravityValue = -9.81f;
    private float _noGravityValue = 0;
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

        TurnOffGravity();
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

    private void TurnOffGravity()
    {
        _currentGravityValue = _noGravityValue;
        _sitting?.Invoke();
    }

    private void UzeGravity()
    {
        if (_playerCollider.IsGrounded && _velosity.y < 0)
        {
            _velosity.y = 0;
            return;
        }

        _velosity.y += _currentGravityValue * Time.fixedDeltaTime;
        _controller.Move(_velosity * Time.fixedDeltaTime);
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
        if (_jumper != null)
            StopCoroutine(_jumper);

        _jumper = StartCoroutine(JumpTaker());
    }
    
    private IEnumerator JumpTaker()
    {
        var waitTime = new WaitForEndOfFrame();

        Vector3 targetHeight = Vector3.up * _jumpForse;
        float heightCounter = 0;
        float jumpTime = 0.3f;

        while (heightCounter < jumpTime)
        {
            heightCounter += Time.deltaTime;
            _controller.Move(targetHeight * Time.deltaTime);
            yield return waitTime;
        }

        if (heightCounter >= jumpTime)
        {
            TurnOffGravity();
            yield break;
        }
    }
}
