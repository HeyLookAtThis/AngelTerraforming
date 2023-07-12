using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundWaterTransition : Transition
{
    private void OnEnable()
    {
        reservoir.Player.FoundWater += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        reservoir.Player.FoundWater -= TurnOnNeedTransit;
    }
}
