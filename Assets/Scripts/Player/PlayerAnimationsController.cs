using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;
    private PlayerColliderController _playerCollider;
    private PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerCollider = GetComponent<PlayerColliderController>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.Falling += PlayIdle;
        _playerCollider.SatOnCloud += PlaySitting;
        _movement.Moved += SetSpeed;
    }

    private void OnDisable()
    {
        _movement.Falling -= PlayIdle;
        _playerCollider.SatOnCloud -= PlaySitting;
        _movement.Moved -= SetSpeed;
    }

    private void PlayIdle()
    {
        _animator.Play(ACPlayer.Stats.Idle);
    }

    private void PlaySitting()
    {
        _animator.Play(ACPlayer.Stats.Sitting);
    }

    private void SetSpeed(float speed)
    {
        _animator.SetFloat(ACPlayer.Params.Speed, speed);
    }
}
