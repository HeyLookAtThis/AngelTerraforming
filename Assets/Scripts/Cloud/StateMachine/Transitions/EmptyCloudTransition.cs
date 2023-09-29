using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Reservoir))]
public class EmptyCloudTransition : Transition
{
    private Reservoir _reservoir;

    private void Awake()
    {
        _reservoir = GetComponent<Reservoir>();
    }

    private void OnEnable()
    {
        _reservoir.WaterIsOver += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        _reservoir.WaterIsOver -= TurnOnNeedTransit;
    }
}
