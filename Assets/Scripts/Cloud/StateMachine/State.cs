using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition _transition;

    protected Cloud cloud;

    protected float speed;
    protected Vector3 targetPosition;
    protected Vector3 positionIndent;

    private float _speedMultiplier = 5;

    protected PlayerMovement Target { get; private set; }

    private void Start()
    {
        speed = Target.Speed * _speedMultiplier;
    }

    public virtual void Enter(PlayerMovement target)
    {
        if(enabled == false)
        {
            enabled = true;
            Target = target;
            cloud = GetComponent<Cloud>();

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
