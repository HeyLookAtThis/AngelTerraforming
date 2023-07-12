using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition _transition;

    protected float speed;
    protected Vector3 targetPosition;
    protected Vector3 positionIndent;

    protected PlayerMovement Target { get; private set; }

    private void Start()
    {
        speed = Target.Speed * 2;
    }

    public void Enter(Player player, PlayerMovement target)
    {
        if(enabled == false)
        {
            enabled = true;

            Target = target;

            _transition.enabled = true;
            _transition.Initialize(player);
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
            _transition.enabled = false;
            enabled = false;
        }
    }
}
