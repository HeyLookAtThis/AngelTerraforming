using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderController : MonoBehaviour
{
    private bool _isGrounded = false;

    private UnityAction<bool> _changedGrounded;
    private UnityAction _foundWater;

    public event UnityAction<bool> ChangedGrounded
    {
        add => _changedGrounded += value;
        remove => _changedGrounded -= value;
    }

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public bool IsGrounded => _isGrounded;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            _foundWater?.Invoke();

        if (hit.collider.TryGetComponent<Plant>(out Plant plant) || hit.collider.TryGetComponent<Ground>(out Ground ground))
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                _changedGrounded?.Invoke(_isGrounded);
            }
        }
        else
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                _changedGrounded?.Invoke(_isGrounded);
            }
        }
    }
}
