using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderController : MonoBehaviour
{
    private bool _isGrounded = false;

    private UnityAction _foundWater;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public bool IsGrounded => _isGrounded;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
        {
            _isGrounded = false;
            _foundWater?.Invoke();
        }


        if (hit.collider.TryGetComponent<Plant>(out Plant plant) || hit.collider.TryGetComponent<Ground>(out Ground ground))
            _isGrounded = true;
    }
}
