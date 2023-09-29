using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderController : MonoBehaviour
{
    private bool _grounded;

    private UnityAction _foundWater;

    public bool IsGrounded => _grounded;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
        {
            _grounded = false;
            _foundWater?.Invoke();
        }

        if (hit.collider.TryGetComponent<Ground>(out Ground ground) || hit.collider.TryGetComponent<Grass>(out Grass grass))
            _grounded = true;
    }
}
