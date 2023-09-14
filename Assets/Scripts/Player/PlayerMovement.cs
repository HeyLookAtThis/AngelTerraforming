using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _cloudLayerMask;
    [SerializeField] private Transform _pivot;

    private CharacterController _controller;
    private Coroutine _jumper;
    private PlayerColliderController _playerCollider;

    private Vector3 _velosity;
    private Vector3 _direction;

    private float _sphereRadius = 0.15f;

    private float _gravityValue = -9.81f;
    private float _noGravityValue = 0;
    private float _currentGravityValue;

    private UnityAction<float> _moved;
    private UnityAction _falling;

    public float Speed => _speed;

    public event UnityAction<float> Moved
    {
        add => _moved += value;
        remove => _moved -= value;
    }

    public event UnityAction Falling
    {
        add => _falling += value;
        remove => _falling -= value;
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerCollider = GetComponent<PlayerColliderController>();

        TurnOffGravity();
    }

    private void OnEnable()
    {
        _playerCollider.FoundWater += OnJumpOnCloud;
    }

    private void OnDisable()
    {
        _playerCollider.FoundWater -= OnJumpOnCloud;
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

    private void OnJumpOnCloud()
    {
        TurnOffGravity();
        BeginToJump();
    }

    public void TurnOnGravity()
    {
        _currentGravityValue = _gravityValue;
        _falling?.Invoke();
    }

    private void TurnOffGravity()
    {
        _currentGravityValue = _noGravityValue;
    }

    private void UzeGravity()
    {
        if (_playerCollider.IsGrounded && _velosity.y < 0)
            _velosity.y = 0;

        _velosity.y += _currentGravityValue * Time.fixedDeltaTime;
        _controller.Move(_velosity * Time.fixedDeltaTime);
    }

    private void Move()
    {
        _direction = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));

        if (_playerCollider.IsGrounded && _direction != Vector3.zero)
            _moved?.Invoke(_speed);
        else
            _moved?.Invoke(0);

        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_direction != Vector3.zero)
            transform.forward = _direction;
    }

    private void BeginToJump()
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
            yield break;
    }
}
