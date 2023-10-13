using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud), typeof(Reservoir))]
public class Scanner : MonoBehaviour
{
    private float _radius;
    private float _yPosition;
    private bool _isActivated;

    private Collider[] _colliders;
    private Reservoir _reservoir;
    private Cloud _cloud;

    private Vector3 _spherePosition;
    private Vector3 _nextSpherePosition;

    private UnityAction _foundWater;
    private UnityAction _foundDryPlant;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction FoundDryPlant
    {
        add => _foundDryPlant += value;
        remove => _foundDryPlant -= value;
    }

    private void Start()
    {
        _reservoir = GetComponent<Reservoir>();
        _cloud = GetComponent<Cloud>();

        _radius =_cloud.Level;
        _yPosition = 1;

        _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);
        _colliders = Physics.OverlapSphere(_spherePosition, _radius);
    }

    private void Update()
    {
        if(_isActivated)
        {
            _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);

            TryCallWaterEvent(_colliders);

            if (_spherePosition != _nextSpherePosition)
            {
                _colliders = GetSubstractColliders();
                CheckColliders(_colliders);

                _nextSpherePosition = _spherePosition;
                _colliders = Physics.OverlapSphere(_nextSpherePosition, _radius);
            }
        }
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
    }

    private Collider[] GetSubstractColliders()
    {
        return Physics.OverlapSphere(_spherePosition, _radius).Except(_colliders).ToArray();
    }

    private void TryCallWaterEvent(Collider[] colliders)
    {
        var waterCollider = colliders.Where(collider => collider.TryGetComponent<Water>(out Water water)).FirstOrDefault();

        if(waterCollider != null)
            _foundWater?.Invoke();
    }

    private void CheckColliders(Collider[] colliders)
    {
        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Plant>(out var plant))
            {
                if (plant.IsGreen == false)
                {
                    _foundDryPlant?.Invoke();

                    if (_reservoir.HaveWater)
                        plant.MakeGreen();
                }
            }
        }
    }
}
