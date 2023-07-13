using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CloudReservoir))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public PlayerMovement Target { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; private set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Initialize(PlayerMovement player)
    {
        Target = player;
    }

    protected void TurnOnNeedTransit()
    {
        NeedTransit = true;
    }
}
