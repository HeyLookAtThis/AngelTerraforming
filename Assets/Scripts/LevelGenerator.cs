using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(Terrain))]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GrassPainter _grassPainter;
    [SerializeField] private List<Tree> _treePrefabs;

    private int _counterCycleOfTree;
    private int _distanceBetwenObjects;

    private int _indentation;
    private int _yAxisValue;
    private Vector3Int _levelStart;
    private Vector3Int _levelEnd;

    private void Start()
    {
        _counterCycleOfTree = 5;
        _distanceBetwenObjects = 20;

        _indentation = 5;
        _yAxisValue = 1;
        GetLevelCoordinateBoundary();

        InstantiateTrees();
    }

    private void GetLevelCoordinateBoundary()
    {
        _levelEnd = new Vector3Int((int)GetComponent<Terrain>().terrainData.size.x - _indentation, _yAxisValue, (int)GetComponent<Terrain>().terrainData.size.z - _indentation);
        _levelStart = new Vector3Int(_indentation, _yAxisValue, _indentation);
    }

    private Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3Int position = new Vector3Int();

        while (isSuccess != true)
        {
            position = new Vector3Int(Random.Range(_levelStart.x, _levelEnd.x), _yAxisValue, Random.Range(_levelStart.z, _levelEnd.z));
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

    private bool CanInstantiateObject(Vector3Int coordinate)
    {
        Vector3Int origin = new Vector3Int(coordinate.x, coordinate.y + _yAxisValue, coordinate.z);

        if(IsEmptyGround(origin) && IsRequiredDistanceFromAdgeLevel(origin))
        {
            float distance = (float)_distanceBetwenObjects;
            float cycleStep = 0.1f;
            float leftDirection = -1f;
            float rightDirection = 1f;

            for (float i = leftDirection; i <= rightDirection; i += cycleStep)
            {
                float zCoordinate;

                if (i < 0)
                    zCoordinate = 1 + i;
                else
                    zCoordinate = 1 - i;

                Vector3 direction = new Vector3(i, 0, zCoordinate);

                Physics.Raycast(origin, direction, out RaycastHit hit, distance);

                if (hit.collider != null)
                    return false;
            }

            for (float i = rightDirection; i >= leftDirection; i -= cycleStep)
            {
                float zCoordinate;

                if (i > 0)
                    zCoordinate = i - 1;
                else
                    zCoordinate = 1 - i;

                Vector3 direction = new Vector3(i, 0, zCoordinate);

                Physics.Raycast(origin, direction, out RaycastHit hit, distance);

                if (hit.collider != null)
                    return false;
            }

            /*
            Vector3.left (-1.0.0)
            Vector3.foreward (0.0.1)
            Vector3.right (1.0.0)
            Veector3.back (0.0.-1)
            */
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool IsEmptyGround(Vector3Int position)
    {
        Physics.Raycast(position, Vector3.down, out RaycastHit hit);

        if(hit.collider.TryGetComponent<Ground>(out Ground ground))
            return true;

        return false;
    }

    private bool IsRequiredDistanceFromAdgeLevel(Vector3Int position)
    {
        for (int i = -_distanceBetwenObjects; i <= _distanceBetwenObjects; i++)
        {
            for (int j = _distanceBetwenObjects; j >= -_distanceBetwenObjects; j--)
            {
                Vector3Int checkingCoordinate = new Vector3Int(position.x + i, position.y, position.z + j);

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
        int treeCount = _counterCycleOfTree;

        while(treeCount > 0)
        {
            foreach(Tree tree in _treePrefabs)
            {
                Tree instantiatedTree = Instantiate(tree, GetRandomCoordinate(), Quaternion.identity, transform);

                instantiatedTree.transform.Rotate(GetRandomRotation());

                instantiatedTree.Initialize(_grassPainter);
                Debug.Log(instantiatedTree);
            }

            treeCount--;
        }
    }
}
