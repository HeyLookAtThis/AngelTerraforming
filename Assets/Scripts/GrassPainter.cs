using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class GrassPainter : MonoBehaviour
{
    private CapsuleCollider _capsuleCollider;

    private UnityAction _foundWater;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _capsuleCollider.isTrigger = true;
        _capsuleCollider.enabled = false;
    }

    public void Activate()
    {
        _capsuleCollider.enabled = true;
    }

    public void Deactivate()
    {
        _capsuleCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Grass>(out var grass))
        {
            grass.Grow();
        }

        if (other.TryGetComponent<Flower>(out var flower))
        {
            flower.TurnOnVisible();
        }

        if (other.TryGetComponent<Water>(out var water))
        {
            _foundWater?.Invoke();
        }
    }
}
