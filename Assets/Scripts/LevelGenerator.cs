using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GrassPainter _grassPainter;
    [SerializeField] private List<Tree> _treesPrefabs;
    [SerializeField] private List<Flower> _flowersPrefabs;
    [SerializeField] private Transform _treesContainer;

    private int _indentation;
    private int _yAxisValue;

    private List<Vector3> _treesPositions;
    private Vector3 _levelStart;
    private Vector3 _levelEnd;

    private void Start()
    {
        _treesPositions = new List<Vector3>();
        _indentation = 5;
        _yAxisValue = 1;

        GetLevelCoordinateBoundary();
        InstantiateTrees();
    }

    private void GetLevelCoordinateBoundary()
    {
        _levelEnd = new Vector3(GetComponent<Terrain>().terrainData.size.x - _indentation, _yAxisValue, GetComponent<Terrain>().terrainData.size.z - _indentation);
        _levelStart = new Vector3(_indentation, _yAxisValue, _indentation);
    }

    private void InstantiateTrees()
    {
        int treeCount = 15;

        while (treeCount > 0)
        {
            foreach (var tree in _treesPrefabs)
            {
                Vector3 treePosition = GetRandomCoordinate();

                Instantiate(tree, treePosition, Quaternion.identity, _treesContainer).Initialize(_grassPainter);
                _treesPositions.Add(treePosition);
                treeCount--;

                if(treeCount == 0)
                    break;
            }
        }

        _treesPositions.Clear();
    }

    private Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3 position = new Vector3();

        while (isSuccess != true)
        {
            position = new Vector3(Random.Range(_levelStart.x, _levelEnd.x), _yAxisValue, Random.Range(_levelStart.z, _levelEnd.z));
            isSuccess = IsRequiredPosition(position);
        }

        return position;
    }

    private bool IsRequiredPosition(Vector3 coordinates)
    {
        int objectsDistance = 5;

        for (int i = -objectsDistance; i <= objectsDistance; i++)
        {
            for (int j = objectsDistance; j >= -objectsDistance; j--)
            {
                Vector3 checkingCoordinate = new Vector3(coordinates.x + i, coordinates.y + _yAxisValue, coordinates.z + j);

                if (IsStayInBoundaries(checkingCoordinate) == false)
                    return false;

                else if (IsEmptyGround(checkingCoordinate) == false)
                    return false;

                else if(IsRequiredDistanceBetweenObjects(checkingCoordinate, objectsDistance) == false)
                    return false;
            }
        }

        return true;
    }

    private bool IsStayInBoundaries(Vector3 checkingCoordinate)
    {
        if (checkingCoordinate.x >= _levelEnd.x || checkingCoordinate.x <= _levelStart.x)
            return false;
        else if (checkingCoordinate.z >= _levelEnd.z || checkingCoordinate.z <= _levelStart.z)
            return false;

        return true;
    }

    private bool IsEmptyGround(Vector3 position)
    {
        Physics.Raycast(position, Vector3.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }

    private bool IsRequiredDistanceBetweenObjects(Vector3 checkingCoordinate, int objectsDistance)
    {
        foreach (var treePosition in _treesPositions)
            if (Vector3.Distance(treePosition, checkingCoordinate) < objectsDistance)
                return false;

        return true;
    }
}
