using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition _transition;

    private Cloud _cloud;

    protected float speed;
    protected Vector3 targetPosition;
    protected Vector3 positionIndent;

    private float _speedMultiplier = 5;

    protected PlayerMovement Target { get; private set; }

    public Cloud Cloud => _cloud;

    private void Start()
    {
        _cloud = GetComponent<Cloud>();

        speed = Target.Speed * _speedMultiplier;
    }

    public virtual void Enter(PlayerMovement target)
    {
        if(enabled == false)
        {
            enabled = true;
            Target = target;

            _transition.enabled = true;
            _transition.Initialize(target);
        }
    }

    public State GetNext()
    {
        if (_transition.NeedTransit)
            return _transition.TargetState;

        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            _transition.TurnOffNeedTransit();
            _transition.enabled = false;
            enabled = false;
        }
    }
}
