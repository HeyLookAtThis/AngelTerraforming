using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCloudTransition : Transition
{
    private void OnEnable()
    {
        reservoir.WaterIsOver += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        reservoir.WaterIsOver -= TurnOnNeedTransit;
    }
}
