using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderController : MonoBehaviour
{
    private Collider _previousCollider;

    private bool _grounded;
    private bool _isOnCloud;

    private UnityAction _foundWater;
    private UnityAction _foundGround;
    private UnityAction _satOnCloud;

    public bool IsGrounded => _grounded;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction FoundGround
    {
        add => _foundGround += value;
        remove => _foundGround -= value;
    }

    public event UnityAction SatOnCloud
    {
        add => _satOnCloud += value;
        remove => _satOnCloud -= value;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            TryCallAction(hit.collider, _foundWater);

        if (hit.collider.TryGetComponent<Ground>(out Ground ground))
        {
            _grounded = true;
            Debug.Log(_grounded);
            Debug.Log(_previousCollider);

        }

        if (hit.collider.TryGetComponent<Cloud>(out Cloud cloud))
            TryCallAction(hit.collider, _satOnCloud);
    }

    private void TryCallAction(Collider collider, UnityAction action)
    {
        if (_previousCollider != collider)
        {
            action?.Invoke();
            _previousCollider = collider;
        }
    }
}
