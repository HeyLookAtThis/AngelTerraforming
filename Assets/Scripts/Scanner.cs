using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Scanner : MonoBehaviour
{
    private float _radius;
    private float _yPosition;
    private bool _isActivated;

    private Collider[] _colliders;
    private Vector3 _spherePosition;
    private Vector3 _nextSpherePosition;

    private UnityAction _foundWater;
    private UnityAction _waterIsOver;
    private UnityAction _foundDryPlant;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }

    public event UnityAction FoundDryPlant
    {
        add => _foundDryPlant += value;
        remove => _foundDryPlant -= value;
    }

    private void Start()
    {
        _yPosition = 1;
        _radius = GetComponentInParent<Cloud>().Level;

        _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);
        _colliders = Physics.OverlapSphere(_spherePosition, _radius);
    }

    private void Update()
    {
        if(_isActivated)
        {
            _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);

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
                _foundWater?.Invoke();
            }
            else
            {
                _waterIsOver?.Invoke();

                collider.TryGetComponent<Plant>(out var plant);

                if (plant.IsGreen == false)
                {
                    _foundDryPlant?.Invoke();
                    plant.MakeGreen();
                }
            }

            Debug.Log(colliders);
        }
    }
}
