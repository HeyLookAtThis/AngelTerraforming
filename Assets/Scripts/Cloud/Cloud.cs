using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    public Player Player => _player;

    public PlayerMovement PlayerMovement => _playerMovement;

    private UnityAction _foundWater;
    private UnityAction _foundGround;

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

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);

        CheckCollider(hit);
    }

    private void CheckCollider(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Water>(out Water water))
        {
            _foundWater?.Invoke();
            Debug.Log("вода");
        }
        else if (hit.collider.TryGetComponent<Ground>(out Ground ground))
        {
            _foundGround?.Invoke();
            Debug.Log("земля");
        }
    }
}
