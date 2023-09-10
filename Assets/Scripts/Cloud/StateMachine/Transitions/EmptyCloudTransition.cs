using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCloudTransition : Transition
{
    private WaterReservoir _reservoir;

    private void Awake()
    {
        _reservoir = GetComponent<WaterReservoir>();
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
