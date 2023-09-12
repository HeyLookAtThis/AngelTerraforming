using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GrassPainter _grassPainter;
    [SerializeField] private List<Tree> _treePrefabs;

    private int _counterCycleOfTree;
    private float _distanceBetwenObjects;

    private int _indentation;
    private float _yAxisValue;
    private Vector3 _levelStart;
    private Vector3 _levelEnd;

    private void Start()
    {
        _counterCycleOfTree = 5;
        _distanceBetwenObjects = 10;

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
        Vector3 origin = new Vector3(coordinate.x, coordinate.y + _yAxisValue, coordinate.z);
        Physics.Raycast(origin, Vector3.down, out RaycastHit hit);

        if(hit.collider.TryGetComponent<Ground>(out Ground ground))
            return true;

        return false;
    }

    private void InstantiateTrees()
    {
        int treeCount = _counterCycleOfTree;
        Debug.Log($"first count {treeCount}");

        while(treeCount > 0)
        {
            foreach(Tree tree in _treePrefabs)
            {
                Tree instantiatedTree = Instantiate(tree, GetRandomCoordinate(), Quaternion.identity, transform);

                instantiatedTree.transform.Rotate(GetRandomRotation());

                instantiatedTree.Initialize(_grassPainter);
            }

            treeCount--;
        }
    }
}
