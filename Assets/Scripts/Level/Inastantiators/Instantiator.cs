using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public abstract class Instantiator : MonoBehaviour
{
    [SerializeField] private float _objectDistance;
    [SerializeField] private Transform _container;

    private static List<Vector3> _positions = new List<Vector3>();

    private Ground _ground;

    public Transform Container => _container;
    
    public float Distance => _objectDistance;

    public Ground Ground => _ground;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
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
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }

    protected Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3 position = new Vector3();

        while (isSuccess != true)
        {
            position = new Vector3(Random.Range(Ground.StartingCoordinate.x, Ground.EndingCoordinate.x), Ground.StartingCoordinate.y, Random.Range(Ground.StartingCoordinate.z, Ground.EndingCoordinate.z));
            isSuccess = IsRequiredPosition(position);
        }

        return position;
    }

    protected void AddObjectCoordinate(Vector3 coordinate)
    {
        _positions.Add(coordinate);
    }

    protected void ClearObjectsCoordinates()
    {
        _positions.Clear();
    }

    private bool IsRequiredPosition(Vector3 coordinates)
    {
        int objectsDistance = (int)_objectDistance;
        float rayOriginHeight = 1f;

        for (int i = -objectsDistance; i <= _objectDistance; i += objectsDistance)
        {
            for (int j = objectsDistance; j >= -_objectDistance; j -= objectsDistance)
            {
                Vector3 checkingCoordinate = new Vector3(coordinates.x + i, coordinates.y, coordinates.z + j);

                if (IsStayInBoundaries(checkingCoordinate) == false)
                    return false;

                else if (IsEmptyGround(checkingCoordinate, rayOriginHeight) == false)
                    return false;

                else if (IsRequiredDistanceBetweenObjects(checkingCoordinate, objectsDistance) == false)
                    return false;
            }
        }

        return true;
    }

    private bool IsRequiredDistanceBetweenObjects(Vector3 checkingCoordinate, int objectsDistance)
    {
        foreach (var plantPosition in _positions)
            if (Vector3.Distance(plantPosition, checkingCoordinate) < objectsDistance)
                return false;

        return true;
    }

    private bool IsStayInBoundaries(Vector3 checkingCoordinate)
    {
        if (checkingCoordinate.x >= Ground.EndingCoordinate.x || checkingCoordinate.x <= Ground.StartingCoordinate.x)
            return false;
        else if (checkingCoordinate.z >= Ground.EndingCoordinate.z || checkingCoordinate.z <= Ground.StartingCoordinate.z)
            return false;

        return true;
    }
}
