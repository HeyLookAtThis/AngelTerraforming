using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Collider _previousCollider;

    private UnityAction _foundWater;
    private UnityAction _foundGround;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction FoundGround
    {
        add=> _foundGround += value;
        remove => _foundGround -= value;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
            TryCallAction(hit.collider, _foundWater);

        if (hit.collider.TryGetComponent<Ground>(out Ground ground))
            TryCallAction(hit.collider, _foundGround);
    }

    private void TryCallAction(Collider collider, UnityAction action)
    {
        if(_previousCollider != collider)
        {
            action?.Invoke();
            _previousCollider = collider;
        }
    }
}
