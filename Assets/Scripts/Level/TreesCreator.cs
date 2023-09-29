using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public class TreesCreator : MonoBehaviour
{
    [SerializeField] private List<Tree> _prefabs;
    [SerializeField] private Transform _container;

    private Ground _ground;
    private List<Vector3> _positions;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _positions = new List<Vector3>();
    }

    private void Start()
    {
        InstantiateTrees();
    }

    private void InstantiateTrees()
    {
        int treeCount = 15;

        while (treeCount > 0)
        {
            foreach (var tree in _prefabs)
            {
                Vector3 treePosition = GetRandomCoordinate();

                Instantiate(tree, treePosition, Quaternion.identity, _container);
                _positions.Add(treePosition);
                treeCount--;

                if(treeCount == 0)
                    break;
            }
        }

        _positions.Clear();
    }

    private Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3 position = new Vector3();

        while (isSuccess != true)
        {
            position = new Vector3(Random.Range(_ground.StartingCoordinate.x, _ground.EndingCoordinate.x), _ground.StartingCoordinate.y, Random.Range(_ground.StartingCoordinate.z, _ground.EndingCoordinate.z));
            isSuccess = IsRequiredPosition(position);
        }

        return position;
    }

    private bool IsRequiredPosition(Vector3 coordinates)
    {
        const int ObjectsDistance = 5;

        for (int i = -ObjectsDistance; i <= ObjectsDistance; i += ObjectsDistance)
        {
            for (int j = ObjectsDistance; j >= -ObjectsDistance; j -= ObjectsDistance)
            {
                Vector3 checkingCoordinate = new Vector3(coordinates.x + i, coordinates.y, coordinates.z + j);

                if (IsStayInBoundaries(checkingCoordinate) == false)
                    return false;

                else if (IsEmptyGround(checkingCoordinate) == false)
                    return false;

                else if (IsRequiredDistanceBetweenObjects(checkingCoordinate, ObjectsDistance) == false)
                    return false;
            }
        }

        return true;
    }

    private bool IsStayInBoundaries(Vector3 checkingCoordinate)
    {
        if (checkingCoordinate.x >= _ground.EndingCoordinate.x || checkingCoordinate.x <= _ground.StartingCoordinate.x)
            return false;
        else if (checkingCoordinate.z >= _ground.EndingCoordinate.z || checkingCoordinate.z <= _ground.StartingCoordinate.z)
            return false;

        return true;
    }

    private bool IsEmptyGround(Vector3 position)
    {
        const float RayOriginHeight = 0.2f;

        Vector3 rayPoint = new Vector3(position.x, position.y + RayOriginHeight, position.z);
        Physics.Raycast(rayPoint, Vector3.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }

    private bool IsRequiredDistanceBetweenObjects(Vector3 checkingCoordinate, int objectsDistance)
    {
        foreach (var treePosition in _positions)
            if (Vector3.Distance(treePosition, checkingCoordinate) < objectsDistance)
                return false;

        return true;
    }
}
