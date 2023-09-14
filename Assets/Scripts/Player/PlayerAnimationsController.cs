using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.Falling += PlayIdle;
        _movement.Sitting += PlaySitting;
        _movement.Runnibg += SetSpeed;
    }

    private void OnDisable()
    {
        _movement.Falling -= PlayIdle;
        _movement.Sitting -= PlaySitting;
        _movement.Runnibg -= SetSpeed;
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
