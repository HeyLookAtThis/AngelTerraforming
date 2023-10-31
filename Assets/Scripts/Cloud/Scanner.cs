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
    private UnityAction _lostWater;
    private UnityAction _foundDryPlant;

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    public event UnityAction LostWater
    {
        add => _lostWater += value;
        remove => _lostWater -= value;
    }

    public event UnityAction FoundDryPlant
    {
        add => _foundDryPlant += value;
        remove => _foundDryPlant -= value;
    }

    private void Awake()
    {
        _reservoir = GetComponent<Reservoir>();
        _cloud = GetComponent<Cloud>();

        _radius = 1;
        _yPosition = 1;

        _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);
        _colliders = Physics.OverlapSphere(_spherePosition, _radius);
    }

    private void OnEnable()
    {
        _cloud.LevelStarter.Beginning += Activate;
        _cloud.LevelFinisher.Begun += Deactivate;
    }

    private void OnDisable()
    {
        _cloud.LevelStarter.Beginning -= Activate;
        _cloud.LevelFinisher.Begun -= Deactivate;
    }

    private void Update()
    {
        Debug.Log(_isActivated);

        if(_isActivated)
        {
            _spherePosition = new Vector3(transform.position.x, _yPosition, transform.position.z);

            if (IsContainsWater())
                _foundWater?.Invoke();
            else
                _lostWater?.Invoke();

            if (_spherePosition != _nextSpherePosition)
            {
                _colliders = GetSubstractColliders();
                CheckColliders();

                _nextSpherePosition = _spherePosition;
                _colliders = Physics.OverlapSphere(_nextSpherePosition, _radius);
            }
        }
    }

    public void Activate()
    {
        ClearColliders();
        _isActivated = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
        ClearColliders();
    }

    private void ClearColliders()
    {
        _colliders = new Collider[0];
    }

    public bool IsContainsWater()
    {
        var waterCollider = _colliders.Where(collider => collider.TryGetComponent<Water>(out Water water)).FirstOrDefault();

        if (waterCollider != null)
            return true;

        return false;
    }

    private Collider[] GetSubstractColliders()
    {
        return Physics.OverlapSphere(_spherePosition, _radius).Except(_colliders).ToArray();
    }

    private void CheckColliders()
    {
        foreach(var collider in _colliders)
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

            if (collider.TryGetComponent<Coin>(out var coin))
                coin.TurnOn();

            if (collider.TryGetComponent<Volcano>(out var volcano))
            {
                if (_cloud.Player.HaveCristall && volcano.IsFrozen == false)
                {
                    volcano.Freeze();
                    _cloud.Player.RemoveIceCristall();
                }
            }
        }
    }
}
