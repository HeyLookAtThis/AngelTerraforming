using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tileBase;

    private Coroutine _cellFiller;
    private Vector3Int _fieldPosition;

    private UnityAction _fieldFilled;

    public event UnityAction FieldFilled
    {
        add => _fieldFilled += value;
        remove => _fieldFilled -= value;
    }

    private void OnEnable()
    {
        _cloud.FoundEmptyGround += OnBeginFillCell;
    }

    private void OnDisable()
    {
        _cloud.FoundEmptyGround -= OnBeginFillCell;
    }

    public bool IsFieldOccupied(Vector3 point)
    {
        _fieldPosition = _tilemap.WorldToCell(point);

        if (_tilemap.GetTile(_fieldPosition) == _tileBase)
            return true;

        return false;    
    }

    private void FillField(Vector3Int position)
    {
        if (_tilemap.GetTile(position) != _tileBase)
        {
            _tilemap.BoxFill(position, _tileBase, position.x, position.y, position.x, position.y);
            _fieldFilled?.Invoke();
        }
    }

    private void OnBeginFillCell()
    {
        if (_cellFiller != null)
            StopCoroutine(_cellFiller);

        _cellFiller = StartCoroutine(CellFiller());
    }

    private IEnumerator CellFiller()
    {
        float seconds = 0.01f;
        var waitTime = new WaitForSeconds(seconds / _cloud.Level);

        FillField(_fieldPosition);
        Debug.Log(_fieldPosition);

        for (int i = -_cloud.Level; i <= _cloud.Level; i++)
        {
            for (int j = _cloud.Level; j >= -_cloud.Level; j--)
            {
                if (-_cloud.Level <= i + j && i + j <= _cloud.Level)
                {
                    Vector3Int neighborFieldPosition = new Vector3Int(_fieldPosition.x + i, _fieldPosition.y + j, _fieldPosition.z);
                    FillField(neighborFieldPosition);

                    Debug.Log($"{i}, {j}");

                    yield return waitTime;
                }
            }

            if (i > _cloud.Level)
                yield break;
        }
    }
}
