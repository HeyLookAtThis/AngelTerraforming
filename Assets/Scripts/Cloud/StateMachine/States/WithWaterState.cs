using UnityEngine;

public class WithWaterState : State
{
    private Resizer _resizer;
    private Cloud _cloud;

    private float _fullsize = 16f;

    private void Awake()
    {
        _resizer = GetComponent<Resizer>();
        _cloud = GetComponent<Cloud>();
    }

    private void Update()
    {
        positionIndent.y = _fullsize * _resizer.CurrentSize;

        targetPosition = _cloud.TargetPlace.position - positionIndent;
        transform.position = targetPosition;

        transform.forward = Target.transform.forward;
    }

    public override void Enter(PlayerMovement target)
    {
        base.Enter(target);

        target.TurnOffGravity();
        cloud.TurnOnLocationUnderPlayer();
    }
}
