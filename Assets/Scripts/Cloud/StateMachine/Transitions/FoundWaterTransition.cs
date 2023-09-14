using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public class FoundWaterTransition : Transition
{
    private Cloud _cloud;

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
    }

    private void OnEnable()
    {
        _cloud.PlayerCollider.FoundWater += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        _cloud.PlayerCollider.FoundWater -= TurnOnNeedTransit;
    }
}
