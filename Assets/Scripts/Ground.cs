using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class Ground : MonoBehaviour
{
    private int _indentation;
    private int _yAxisValue;

    private Terrain _terrain;

    public Vector3 StartingCoordinate { get; private set; }

    public Vector3 EndingCoordinate {  get; private set; }

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();

        _indentation = 5;
        _yAxisValue = 1;
        SetLevelBoundary();
    }

    public float GetArea()
    {
        return _terrain.terrainData.size.x * _terrain.terrainData.size.z;
    }

    private void SetLevelBoundary()
    {
        EndingCoordinate = new Vector3(_terrain.terrainData.size.x - _indentation, _yAxisValue, _terrain.terrainData.size.z - _indentation);
        StartingCoordinate = new Vector3(_indentation, _yAxisValue, _indentation);
    }
}
