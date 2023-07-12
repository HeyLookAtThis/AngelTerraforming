using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CloudReservoir))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected CloudReservoir reservoir;

    public Player Target { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; private set; }

    private void Awake()
    {
        reservoir = GetComponent<CloudReservoir>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Initialize(Player player)
    {
        Target = player;
    }

    protected void TurnOnNeedTransit()
    {
        NeedTransit = true;
    }
}
