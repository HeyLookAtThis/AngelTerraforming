using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NoWaterState : State
{
    private float _distance { get { return Vector3.Distance(Target.transform.position, Target.transform.position + positionIndent); } }

    private Coroutine _positionChanger;

    private Transform _cloud;

    private void Awake()
    {
        positionIndent = new Vector3(-1.5f, 2f, -1.5f);
        _cloud = this.transform;
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;

        if (Vector3.Distance(Target.transform.position, transform.position) > _distance)
        {
            _cloud.DOMoveX(targetPosition.x, speed * Time.deltaTime);
            _cloud.DOMoveZ(targetPosition.z, speed * Time.deltaTime);

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

        cloud.TurnOffLacationUnderPlayer();

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
