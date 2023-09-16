using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GrassPainter _grassPainter;
    [SerializeField] private List<Tree> _treesPrefabs;
    [SerializeField] private List<Flower> _flowersPrefabs;

    private int _treesOfOneTypeCounter;
    private int _flowersOfOneTypeCounter;
    private int _distanceBetwenObjects;

    private int _indentation;
    private int _yAxisValue;

    private List<Vector3> _treesPositions;
    private Vector3 _levelStart;
    private Vector3 _levelEnd;

    private void Start()
    {
        _distanceBetwenObjects = 10;
        _treesOfOneTypeCounter = 7;
        _flowersOfOneTypeCounter = 20;
        _indentation = 5;
        _yAxisValue = 1;
        _treesPositions = new List<Vector3>();

        GetLevelCoordinateBoundary();
        InstantiateTrees();
    }

    private void GetLevelCoordinateBoundary()
    {
        _levelEnd = new Vector3(GetComponent<Terrain>().terrainData.size.x - _indentation, _yAxisValue, GetComponent<Terrain>().terrainData.size.z - _indentation);
        _levelStart = new Vector3(_indentation, _yAxisValue, _indentation);
    }

    private Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3 position = new Vector3();

        while (isSuccess != true)
        {
            position = new Vector3(Random.Range(_levelStart.x, _levelEnd.x), _yAxisValue, Random.Range(_levelStart.z, _levelEnd.z));
            isSuccess = CanInstantiateObject(position);
        }

        return position;
    }

    private Vector3 GetRandomRotation()
    {
        float startAxisValue = 0;
        float endAxisValue = 360;

        return new Vector3(startAxisValue, Random.Range(startAxisValue, endAxisValue), startAxisValue);
    }

    private bool CanInstantiateObject(Vector3 coordinate)
    {
        Vector3 checkingCoordinate = new Vector3(coordinate.x, coordinate.y + _yAxisValue, coordinate.z);

        if(IsEmptyGround(checkingCoordinate) && IsRequiredDistanceFromAdgeLevel(checkingCoordinate))
        {
            foreach (var treePosition in _treesPositions)
                if (Vector3.Distance(treePosition, checkingCoordinate) < _distanceBetwenObjects)
                    return false;
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool IsEmptyGround(Vector3 position)
    {
        Physics.Raycast(position, Vector3.down, out RaycastHit hit);

        if(hit.collider.TryGetComponent<Ground>(out Ground ground))
            return true;

        return false;
    }

    private bool IsRequiredDistanceFromAdgeLevel(Vector3 position)
    {
        for (int i = -_distanceBetwenObjects; i <= _distanceBetwenObjects; i++)
        {
            for (int j = _distanceBetwenObjects; j >= -_distanceBetwenObjects; j--)
            {
                Vector3 checkingCoordinate = new Vector3(position.x + i, position.y, position.z + j);

                if (checkingCoordinate.x >= _levelEnd.x || checkingCoordinate.x <= _levelStart.x)
                    return false;
                else if (checkingCoordinate.z >= _levelEnd.z || checkingCoordinate.z <= _levelStart.z)
                    return false;
            }
        }

        return true;
    }

    private void InstantiateTrees()
    {
        int treesCycleCounter = _treesOfOneTypeCounter;

        while (treesCycleCounter > 0)
        {
            foreach(Tree tree in _treesPrefabs)
            {
                Tree instantiatedTree = Instantiate(tree, GetRandomCoordinate(), Quaternion.identity, transform);

                instantiatedTree.transform.Rotate(GetRandomRotation());

                instantiatedTree.Initialize(_grassPainter);

                _treesPositions.Add(instantiatedTree.transform.position);
                Debug.Log(instantiatedTree);
            }

            treesCycleCounter--;
        }

        _treesPositions.Clear();
    }
}
