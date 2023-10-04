using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NoWaterState : State
{
    private float _distance { get { return Vector3.Distance(Target.transform.position, Target.transform.position + positionIndent); } }

    private Coroutine _positionChanger;

    private void Awake()
    {
        positionIndent = new Vector3(-1.5f, 2f, -1.5f);
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;

        if (Vector3.Distance(Target.transform.position, transform.position) > _distance)
        {
            Move(targetPosition);
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
        var waitTime = new WaitForEndOfFrame();

        cloud.TurnOffLacationUnderPlayer();

        while (transform.position != targetPosition)
        {
            targetPosition = Target.transform.position + positionIndent;
            Move(targetPosition);
            yield return waitTime;
        }

        if (transform.position == targetPosition)
        {
            Debug.Log("Exit");
            yield break;
        }
    }
}
