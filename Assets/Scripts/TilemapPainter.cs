using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tile;
    [SerializeField] private int _distance;

    private Vector3Int _cellPosition;

    private void OnEnable()
    {
        _cloud.FoundEmptyGround += TryFillField;
    }

    private void OnDisable()
    {
        _cloud.FoundEmptyGround -= TryFillField;
    }

    public bool IsFieldOccupied(Vector3 point)
    {
        _cellPosition = _tilemap.WorldToCell(point);

        if (_tilemap.GetTile(_cellPosition) == _tile)
            return true;

        return false;    
    }

    private void TryFillField(bool isFieldOccupied)
    {
        if (isFieldOccupied == false)
            _tilemap.BoxFill(_cellPosition, _tile, _cellPosition.x - _distance, _cellPosition.y - _distance, _cellPosition.x + _distance, _cellPosition.y + _distance);
    }
}
