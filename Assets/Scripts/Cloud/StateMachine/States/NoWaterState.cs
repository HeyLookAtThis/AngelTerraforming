using System.Collections;
using UnityEngine;

public class NoWaterState : State
{
    private Coroutine _positionChanger;

    private void Start()
    {
        float yIndent = 2f;
        positionIndent.y = yIndent;        
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;
        speed = Target.Speed * Vector3.Distance(targetPosition, transform.position);

        Move(targetPosition);
        transform.LookAt(targetPosition);
    }

    public override void Enter(PlayerMovement target)
    {
        base.Enter(target);

        scanner.Deactivate();
        target.TurnOnGravity();
        BeginChangePosition();
    }

    private void BeginChangePosition()
    {
        if (_positionChanger != null)
            StopCoroutine(_positionChanger);

        _positionChanger = StartCoroutine(TakePosition());
    }

    private IEnumerator TakePosition()
    {
        var waitTime = new WaitForEndOfFrame();

        while (transform.position != targetPosition)
        {
            targetPosition = Target.transform.position + positionIndent;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Target.Speed * Time.deltaTime);
            yield return waitTime;
        }

        if (transform.position == targetPosition)
            yield break;
    }
}
