using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public class FoundWaterTransition : Transition
{
    private Cloud _cloud;
    private CloudReservoir _reservoir;

    private void Awake()
    {
        _reservoir = GetComponent<CloudReservoir>();
        _cloud = GetComponent<Cloud>();
    }

    private void OnEnable()
    {
        _reservoir.Player.FoundWater += TurnOnNeedTransit;
        _cloud.FoundWater += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        _reservoir.Player.FoundWater -= TurnOnNeedTransit;
        _cloud.FoundWater -= TurnOnNeedTransit;
    }
}
