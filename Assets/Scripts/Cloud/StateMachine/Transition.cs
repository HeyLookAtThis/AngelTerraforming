using UnityEngine;
using UnityEngine.Events;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public PlayerMovement Target { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; private set; }

    public void Initialize(PlayerMovement player)
    {
        Target = player;
    }

    public void TurnOffNeedTransit()
    {
        NeedTransit = false;
    }

    protected void TurnOnNeedTransit()
    {
        NeedTransit = true;
    }
}
