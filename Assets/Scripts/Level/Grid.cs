using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class Grid : MonoBehaviour
{
    private Terrain _terrain;
    private List<Vector3> _coordinates;

    private Vector3 _startingCoordinate;
    private Vector3 _endingCoordinate;

    private int _objectDistance;
    private float _startCoordinateNumber;
    private float _yAxisValue;

    public Vector3 Start => _startingCoordinate;

    public Vector3 End => _endingCoordinate;

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();
        
        _objectDistance = 5;
        _startCoordinateNumber = 2.5f;
        _yAxisValue = 1;

        SetLevelBoundaries();
        Initialize();
    }

    public Vector3 GetRandomCell()
    {
        Vector3 coordinate = _coordinates[Random.Range(0, _coordinates.Count)];
        _coordinates.Remove(coordinate);
        return coordinate;
    }

    public Vector3 GetRandomCoordinate()
    {
        return new Vector3(Random.Range(_startingCoordinate.x, _endingCoordinate.x), _yAxisValue, Random.Range(_startingCoordinate.z, _endingCoordinate.z));
    }

    private void Initialize()
    {
        _coordinates = new List<Vector3>();

        for (int i = _objectDistance; i < _endingCoordinate.x; i += _objectDistance)
            for (int j = _objectDistance; j < _endingCoordinate.z; j += _objectDistance)
                _coordinates.Add(new Vector3(i, _yAxisValue, j));
    }

    private void SetLevelBoundaries()
    {
        _startingCoordinate = new Vector3(_startCoordinateNumber, _yAxisValue, _startCoordinateNumber);
        _endingCoordinate = new Vector3(_terrain.terrainData.size.x - _startCoordinateNumber, _yAxisValue, _terrain.terrainData.size.z - _startCoordinateNumber);
    }
}
