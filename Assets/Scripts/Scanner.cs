using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cloud))]
public class Scanner : MonoBehaviour
{
    private float _radius;
    private float _yPosition;
    private bool _isActivated;

    private Collider[] _colliders;
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

            if (_cloud.IsAboveWater)
                _foundWater?.Invoke();

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

    private void CheckColliders(Collider[] colliders)
    {
        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Water>(out var water))
            {
                Debug.Log(water);
                _cloud.TurnOnIsAboveWater();
            }
            else if (collider.TryGetComponent<Plant>(out var plant) && collider.TryGetComponent<Water>(out water) == false)
            {
                if (_cloud.IsAboveWater)
                    _cloud.TurnOffIsAboveWater();

                if (plant.IsGreen == false)
                {
                    _foundDryPlant?.Invoke();
                    plant.MakeGreen();
                }
            }
        }
    }
}
