using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Ground))]
public class FlowersCreator : MonoBehaviour
{
    [SerializeField] private uint _flowersCount;
    [SerializeField] private List<Flower> _prefabs;
    [SerializeField] private Transform _container;

    private Ground _ground;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
    }

    private void Start()
    {
        InstantiateFlowers();
    }

    private void InstantiateFlowers()
    {
        uint flowersCount = _flowersCount;

        while (flowersCount > 0)
        {
            foreach (var flower in _prefabs)
            {
                Instantiate(flower, GetRandomCoordinate(), Quaternion.identity, _container);
                flowersCount--;

                if (flowersCount == 0)
                    break;
            }
        }
    }

    private Vector3 GetRandomCoordinate()
    {
        bool isSuccess = false;
        Vector3Int position = new Vector3Int();

        while (isSuccess != true)
        {
            position = new Vector3Int(Random.Range((int)_ground.StartingCoordinate.x, (int)_ground.EndingCoordinate.x), (int)_ground.StartingCoordinate.y, Random.Range((int)_ground.StartingCoordinate.z, (int)_ground.EndingCoordinate.z));
            isSuccess = IsRequiredPosition(position);
        }

        return position;
    }

    private bool IsRequiredPosition(Vector3Int coordinates)
    {
        const int ObjectsDistance = 1;

        for (int i = -ObjectsDistance; i <= ObjectsDistance; i += ObjectsDistance)
        {
            for (int j = ObjectsDistance; j >= -ObjectsDistance; j -= ObjectsDistance)
            {
                Vector3Int checkingCoordinate = new Vector3Int(coordinates.x + i, coordinates.y, coordinates.z + j);

                if (IsStayInBoundaries(checkingCoordinate) == false)
                    return false;

                else if (IsEmptyGround(checkingCoordinate) == false)
                    return false;
            }
        }

        return true;
    }

    private bool IsStayInBoundaries(Vector3Int checkingCoordinate)
    {
        if (checkingCoordinate.x >= _ground.EndingCoordinate.x || checkingCoordinate.x <= _ground.StartingCoordinate.x)
            return false;
        else if (checkingCoordinate.z >= _ground.EndingCoordinate.z || checkingCoordinate.z <= _ground.StartingCoordinate.z)
            return false;

        return true;
    }

    private bool IsEmptyGround(Vector3Int position)
    {
        const int RayOriginHeight = 1;

        Vector3Int rayPoint = new Vector3Int(position.x, position.y + RayOriginHeight, position.z);
        Physics.Raycast(rayPoint, Vector3Int.down, out RaycastHit hit);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
                return true;

        return false;
    }
}
