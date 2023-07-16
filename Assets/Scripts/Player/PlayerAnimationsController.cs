using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.FoundGround += PlayRunning;
        _player.SatOnCloud += PlaySitting;
    }

    private void OnDisable()
    {
        _player.FoundGround -= PlayRunning;
        _player.SatOnCloud -= PlaySitting;
    }

    private void PlayRunning()
    {
        _animator.Play(ACPlayer.Stats.Run);
    }

    private void PlaySitting()
    {
        _animator.Play(ACPlayer.Stats.Sitting);
    }
}
