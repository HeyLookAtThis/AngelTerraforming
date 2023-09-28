using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public class GrassCreator : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Grass _grass;
    [SerializeField] private Transform _container;

    private Ground _ground;

    private void Start()
    {
        _ground = GetComponent<Ground>();
        Instantiate();
    }

    private void Instantiate()
    {
        for (float i = _ground.StartingCoordinate.x; i < _ground.EndingCoordinate.x; i += _grid.cellSize.x)
        {
            for (float j = _ground.StartingCoordinate.z; j < _ground.EndingCoordinate.z; j += _grid.cellSize.y)
            {
                Vector3 position = new Vector3(i, _ground.StartingCoordinate.y, j);

                if (IsEmptyGround(position))
                    Instantiate(_grass, position, Quaternion.identity, _container).TurnOff();
            }
        }
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
}
