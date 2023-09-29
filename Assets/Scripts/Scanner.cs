using UnityEngine;
using UnityEngine.Events;

public class Scanner : MonoBehaviour
{
    private Cloud _cloud;

    private float _radius;
    private float _yPosition;

    private bool _isActive;

    private UnityAction _foundWater;
    private UnityAction<Grass> _foundGrass;
    private UnityAction<Tree> _foundTree;
    private UnityAction<Flower> _foundFlower;

    public event UnityAction<Grass> FoundGrass
    {
        add => _foundGrass += value;
        remove => _foundGrass -= value;
    }

    public event UnityAction<Tree> FoundTree
    {
        add => _foundTree += value;
        remove => _foundTree -= value;
    }

    public event UnityAction<Flower> FoundFlower
    {
        add => _foundFlower += value;
        remove => _foundFlower -= value;
    }

    public event UnityAction FoundWater
    {
        add => _foundWater += value;
        remove => _foundWater -= value;
    }

    private void Start()
    {
        _cloud = GetComponentInParent<Cloud>();

        _yPosition = 1;
    }

    private void Update()
    {
        if (_isActive)
        {
            Vector3 position = new Vector3(_cloud.transform.position.x, _yPosition, _cloud.transform.position.z);
            CheckColliders(Physics.OverlapSphere(position, _radius));
        }        
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public void SetLevel(float level)
    {
        _radius = level;
    }

    private void CheckColliders(Collider[] colliders)
    {
        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Water>(out var water))
                _foundWater?.Invoke();

            if (collider.TryGetComponent<Grass>(out var grass))
                if (grass.Growed == false)
                    _foundGrass?.Invoke(grass);

            if (collider.TryGetComponent<Flower>(out var flower))
                if (flower.IsVisible == false)
                    _foundFlower?.Invoke(flower);

            if (collider.TryGetComponent<Tree>(out Tree tree))
                if (tree.HasGrassAround == false)
                    _foundTree?.Invoke(tree);

            Debug.Log(colliders);
        }
    }
}
