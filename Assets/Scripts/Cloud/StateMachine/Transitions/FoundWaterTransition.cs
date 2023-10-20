using UnityEngine;

[RequireComponent(typeof(Cloud), typeof(Scanner))]
public class FoundWaterTransition : Transition
{
    private Scanner _scanner;
    private Cloud _cloud;

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
        _scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        _cloud.Player.Movement.Sitting += TurnOnNeedTransit;
        _scanner.FoundWater += TurnOnNeedTransit;
    }

    private void OnDisable()
    {
        _cloud.Player.Movement.Sitting -= TurnOnNeedTransit;
        _scanner.FoundWater -= TurnOnNeedTransit;
    }
}
