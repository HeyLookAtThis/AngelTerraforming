using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCloudTransition : Transition
{
    private CloudReservoir _reservoir;

    private void Awake()
    {
        _reservoir = GetComponent<CloudReservoir>();
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
