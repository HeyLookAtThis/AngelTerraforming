using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.Falling += PlayIdle;
        _movement.Sitting += PlaySitting;
        _movement.Runnibg += PlayRun;
    }

    private void OnDisable()
    {
        _movement.Falling -= PlayIdle;
        _movement.Sitting -= PlaySitting;
        _movement.Runnibg -= PlayRun;
    }

    private void PlayIdle()
    {
        _animator.Play(ACPlayer.Stats.Idle);
    }

    private void PlaySitting()
    {
        _animator.Play(ACPlayer.Stats.Sitting);
    }

    private void PlayRun(float speed)
    {
        _animator.SetFloat(ACPlayer.Params.Speed, speed);
    }
}
