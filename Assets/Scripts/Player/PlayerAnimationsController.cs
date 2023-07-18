using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _player.FoundGround += PlayIdle;
        _movement.SatOnCloud += PlaySitting;
        _movement.Moved += SetSpeed;
    }

    private void OnDisable()
    {
        _player.FoundGround -= PlayIdle;
        _movement.SatOnCloud -= PlaySitting;
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
