using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Cloud))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition _transition;

    protected Cloud cloud;
    protected Scanner scanner;

    protected float speed;
    protected Vector3 targetPosition;
    protected Vector3 positionIndent;

    protected PlayerMovement Target { get; private set; }

    public virtual void Enter(PlayerMovement target)
    {
        if(enabled == false)
        {
            enabled = true;
            Target = target;
            cloud = GetComponent<Cloud>();
            scanner = GetComponent<Scanner>();

            _transition.enabled = true;
            _transition.Initialize(target);
        }
    }

    public State GetNext()
    {
        if (_transition.NeedTransit)
            return _transition.TargetState;

        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            _transition.TurnOffNeedTransit();
            _transition.enabled = false;
            enabled = false;
        }
    }

    protected void Move(Vector3 direction)
    {
        cloud.transform.DOMoveX(direction.x, Target.Speed * Time.deltaTime);
        cloud.transform.DOMoveY(direction.y, Target.Speed * Time.deltaTime);
        cloud.transform.DOMoveZ(direction.z, Target.Speed * Time.deltaTime);
    }
}
