using UnityEngine;

public class NoWaterState : State
{
    private float _distance { get { return 1.5f; } }

    private void Awake()
    {
        positionIndent = new Vector3(0, 0.5f, -1.5f);
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;

        if (Vector3.Distance(Target.transform.position, transform.position) > _distance)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
