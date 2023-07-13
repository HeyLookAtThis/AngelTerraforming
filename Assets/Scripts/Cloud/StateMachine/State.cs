using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected float speed;
    protected Vector3 targetPosition;
    protected Vector3 positionIndent;

    protected PlayerMovement Target { get; private set; }

    private void Start()
    {
        speed = Target.Speed * 2;
    }

    public virtual void Enter(Player player, PlayerMovement target)
    {
        if(enabled == false)
        {
            enabled = true;

            Target = target;

            foreach(var transition in _transitions)
            {
                transition.enabled = true;
                transition.Initialize(target);
            }
        }
    }

    public State GetNext()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }
}
