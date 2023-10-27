using System.Collections.Generic;
using UnityEngine;

public class GrassCreator : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private Grass _grass;
    [SerializeField] private Transform _container;
    [SerializeField] private VolcanoDisplayer _displayer;

    private Grid _grid;
    private List<Grass> _grassList = new List<Grass>();
    private float _rayOriginHeight;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _rayOriginHeight = 0.2f;
    }

    private void OnEnable()
    {
        _displayer.Fulled += TurnOffAll;
    }

    private void OnDisable()
    {
        _displayer.Fulled -= TurnOffAll;
    }

    private void Start()
    {
        Create();
    }

    private void Create()
    {
        for (float i = _grid.Start.x; i < _grid.End.x; i += _distance)
        {
            for (float j = _grid.Start.z; j < _grid.End.z; j += _distance)
            {
                Vector3 position = new Vector3(i, _grid.Start.y, j);

                if (IsEmptyGround(position))
                {
                    Grass grass = Instantiate(_grass, position, Quaternion.identity, _container);
                    grass.TurnOff();
                    _grassList.Add(grass);
                }
            }
        }
    }

    private bool IsEmptyGround(Vector3 position)
    {
        Vector3 rayPoint = new Vector3(position.x, position.y + _rayOriginHeight, position.z);
        Physics.Raycast(rayPoint, Vector3.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }

    private void TurnOffAll()
    {
        foreach(var grass in _grassList)
            grass.TurnOff();
    }
}
