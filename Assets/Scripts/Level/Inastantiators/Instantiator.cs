using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid), typeof(Ground))]
public abstract class Instantiator : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private float _waterDistance;

    private LevelCounter _levelCounter;
    protected LevelStarter levelStarter;
    protected LevelFinisher levelFinisher;

    private Water _water;
    private Grid _grid;
    private Ground _ground;

    public Transform Container => _container;    

    public Grid Grid => _grid;

    public LevelCounter LevelGenerator => _levelCounter;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _grid = GetComponent<Grid>();

        _levelCounter = _ground.LevelGenerator;
        levelStarter = _ground.LevelStarter;
        levelFinisher = _ground.LevelFinisher;

        _water = _ground.Water;

        _waterDistance = 5;
    }

    private void OnEnable()
    {
        levelStarter.Beginning += Create;
        levelFinisher.Begun += SetDefaultState;
    }

    private void OnDisable()
    {
        levelStarter.Beginning -= Create;
        levelFinisher.Begun -= SetDefaultState;
    }

    public abstract void Create();

    public abstract void SetDefaultState();

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
