using UnityEngine;

[RequireComponent(typeof(Cloud), typeof(Resizer))]
public class WithWaterState : State
{
    private Resizer _resizer;

    private float _fullsize = 16f;

    private void Awake()
    {
        _resizer = GetComponent<Resizer>();
    }

    private void Update()
    {
        positionIndent.y = _fullsize * _resizer.CurrentValue;

        targetPosition = cloud.TargetPlace.position - positionIndent;

        transform.position = targetPosition;

        transform.forward = Target.transform.forward;
    }

    public override void Enter(PlayerMovement target)
    {
        base.Enter(target);

        scanner.Activate();
        target.TurnOffGravity();
    }
}
