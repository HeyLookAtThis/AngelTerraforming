using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid), typeof(Ground))]
public abstract class Instantiator : MonoBehaviour
{
    [SerializeField] private float _objectDistance;
    [SerializeField] private Transform _container;

    private float _waterDistance;

    private LevelCounter _levelCounter;
    private StartGameButton _startGameButton;
    private Water _water;
    private Grid _grid;

    public Transform Container => _container;
    
    public float Distance => _objectDistance;

    public Grid Grid => _grid;

    public LevelCounter LevelGenerator => _levelCounter;

    private void Awake()
    {
        _levelCounter = GetComponent<Ground>().LevelGenerator;
        _startGameButton = _levelCounter.StartGameButton;
        _grid = GetComponent<Grid>();
        _water = GetComponent<Ground>().Water;
        _waterDistance = 5;
    }

    private void OnEnable()
    {
        _startGameButton.AddAction(Create);
    }

    private void OnDisable()
    {
        _startGameButton.RemoveAction(Create);
    }

    public abstract void Create();

    protected bool IsEmptyGround(Vector3 position, float rayOriginHeight)
    {
        Vector3 rayPoint = new Vector3(position.x, position.y + rayOriginHeight, position.z);
        Physics.Raycast(rayPoint, Vector3.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground) || hit.collider.TryGetComponent<Grass>(out Grass grass))
                return true;

        return false;
    }

    protected Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        float rayOriginHeight = 1f;

        Vector3 position = new Vector3();

        while (isSuccess != true)
        {
            position = Grid.GetRandomCell();

            isSuccess = IsEmptyGround(position, rayOriginHeight) && Vector3.Distance(position, _water.transform.position) > _waterDistance;
        }

        return position;
    }
}
