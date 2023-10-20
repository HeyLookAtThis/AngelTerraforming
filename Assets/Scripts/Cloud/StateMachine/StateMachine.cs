using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;

    public State CurrentState => _currentState;

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNext();

        if(nextState!= null)
            Transit(nextState);
    }

    public void Reset(State state)
    {
        _currentState = state;

        if (_currentState != null)
            _currentState.Enter(GetComponent<Cloud>().Player.Movement);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        Reset(nextState);
    }
}
