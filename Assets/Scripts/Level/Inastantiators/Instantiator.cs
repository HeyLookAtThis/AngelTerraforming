using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public abstract class Instantiator : MonoBehaviour
{
    [SerializeField] private float _objectDistance;
    [SerializeField] private Water _water;
    [SerializeField] private Transform _container;

    private float _waterDistance;
    private Grid _grid;

    private static List<Vector3> _positions = new List<Vector3>();

    public Transform Container => _container;
    
    public float Distance => _objectDistance;

    public Grid Grid => _grid;

    public IReadOnlyList<Vector3> Positions => _positions;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _waterDistance = 5;
    }

    private void Start()
    {
        Create();
    }

    protected abstract void Create();

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

    protected void AddObjectCoordinate(Vector3 coordinate)
    {
        _positions.Add(coordinate);
    }
}
