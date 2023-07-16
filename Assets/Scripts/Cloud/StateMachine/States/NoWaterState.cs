using System.Collections;
using UnityEngine;

public class NoWaterState : State
{
    private float _distance { get { return 2f; } }

    private Coroutine _positionChanger;

    private void Awake()
    {
        positionIndent = new Vector3(0, 1f, -1.5f);
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;

        if (Vector3.Distance(Target.transform.position, transform.position) > _distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(Target.transform.position);
        }
    }

    public override void Enter(PlayerMovement target)
    {
        base.Enter(target);

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
        float seconds = 0.01f;
        var waitTime = new WaitForSeconds(seconds);

        while (transform.position != targetPosition)
        {
            targetPosition = Target.transform.position + positionIndent;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * seconds);
            yield return waitTime;
        }

        if (transform.position == targetPosition)
            yield break;
    }
}
