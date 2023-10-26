using UnityEngine;

public class GrassCreator : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private Grass _grass;
    [SerializeField] private Transform _container;

    private Grid _grid;
    private float _rayOriginHeight;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _rayOriginHeight = 0.2f;
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
                    Instantiate(_grass, position, Quaternion.identity, _container).TurnOff();
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
}
