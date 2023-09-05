using System.Collections;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapPainter : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tileBase;

    private int _distance = 1;
    private Coroutine _cellFiller;

    private Vector3Int _cellPosition;
    private Vector3Int[] _cellsArround;

    private UnityAction _fieldFilled;

    public event UnityAction FieldFilled
    {
        add => _fieldFilled += value;
        remove => _fieldFilled -= value;
    }

    private void OnEnable()
    {
        _cloud.FoundEmptyGround += BeginFillCell;
    }

    private void OnDisable()
    {
        _cloud.FoundEmptyGround -= BeginFillCell;
    }

    public bool IsFieldOccupied(Vector3 point)
    {
        _cellPosition = _tilemap.WorldToCell(point);

        if (_tilemap.GetTile(_cellPosition) == _tileBase)
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

    private Vector3Int[] GetCellsAround()
    {
        Vector3Int topRightCell = new Vector3Int(_cellPosition.x + _distance, _cellPosition.y + _distance, _cellPosition.z);
        Vector3Int rightCell = new Vector3Int(_cellPosition.x + _distance, _cellPosition.y, _cellPosition.z);
        Vector3Int buttomRightCell = new Vector3Int(_cellPosition.x + _distance, _cellPosition.y - _distance, _cellPosition.z);

        Vector3Int topLeftCell = new Vector3Int(_cellPosition.x, _cellPosition.y + _distance, _cellPosition.z);
        Vector3Int leftCell = new Vector3Int(_cellPosition.x - _distance, _cellPosition.y, _cellPosition.z);
        Vector3Int buttomLeftCell = new Vector3Int(_cellPosition.x, _cellPosition.y - _distance, _cellPosition.z);

        return new Vector3Int[] { topRightCell, rightCell, buttomRightCell, topLeftCell, leftCell, buttomLeftCell };
    }

    private void BeginFillCell()
    {
        if (_cellFiller != null)
            StopCoroutine(_cellFiller);

        _cellFiller = StartCoroutine(CellFiller());
    }

    private IEnumerator CellFiller()
    {
        float seconds = 0.02f;
        var waitTime = new WaitForEndOfFrame();
        int cycleCounter = 0;

        FillField(_cellPosition);
        _cellsArround = GetCellsAround();

        for (int i = 0; i < _cellsArround.Length; i++)
        {
            FillField(_cellsArround[i]);
            cycleCounter++;
            yield return waitTime;
        }        

        if (cycleCounter == _cellsArround.Length)
            yield break;
    }
}
