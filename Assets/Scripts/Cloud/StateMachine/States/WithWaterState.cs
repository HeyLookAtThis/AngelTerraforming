using UnityEngine;

public class WithWaterState : State
{
    private void Awake()
    {
        positionIndent = new Vector3(0, 0.4f, 0.2f);
    }

    private void Update()
    {
        targetPosition = Target.transform.position + positionIndent;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        transform.forward = Target.transform.forward;
    }
}
