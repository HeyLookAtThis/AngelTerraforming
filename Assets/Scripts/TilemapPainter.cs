using System.Collections;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tile;

    private Coroutine _cellFiller;
    private Vector3Int _cellPosition;
    private Vector3Int[] _neighborCells;

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

        if (_tilemap.GetTile(_cellPosition) == _tile)
            return true;

        return false;    
    }

    private void FillField(Vector3Int cell)
    {
        if (_tilemap.GetTile(cell) != _tile)
        {
            _tilemap.BoxFill(cell, _tile, cell.x, cell.y, cell.x, cell.y);
            _fieldFilled?.Invoke();
        }
    }

    private Vector3Int[] GetNeighborCells()
    {
        int cellStep = 1;

        Vector3Int topCell = new Vector3Int(_cellPosition.x, _cellPosition.y + cellStep, _cellPosition.z);
        Vector3Int buttomCell = new Vector3Int(_cellPosition.x, _cellPosition.y - cellStep, _cellPosition.z);

        Vector3Int rightCell = new Vector3Int(_cellPosition.x + cellStep, _cellPosition.y, _cellPosition.z);
        Vector3Int leftCell = new Vector3Int(_cellPosition.x - cellStep, _cellPosition.y, _cellPosition.z);

        Vector3Int topRightCell = new Vector3Int(_cellPosition.x + cellStep, _cellPosition.y + cellStep, _cellPosition.z);
        Vector3Int topLeftCell = new Vector3Int(_cellPosition.x - cellStep, _cellPosition.y + cellStep, _cellPosition.z);

        Vector3Int buttomRightCell = new Vector3Int(_cellPosition.x + cellStep, _cellPosition.y - cellStep, _cellPosition.z);
        Vector3Int buttomLeftCell = new Vector3Int(_cellPosition.x - cellStep, _cellPosition.y - cellStep, _cellPosition.z);

        return new Vector3Int[] { _cellPosition, topCell, buttomCell, rightCell, leftCell, topRightCell, topLeftCell, buttomRightCell, buttomLeftCell };
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
        var waitTime = new WaitForSeconds(seconds);
        int cycleCounter = 0;

        _neighborCells = GetNeighborCells();

        for (int i = 0; i < _neighborCells.Length; i++)
        {
            FillField(_neighborCells[i]);
            cycleCounter++;
            yield return waitTime;
        }        

        if (cycleCounter == _neighborCells.Length)
            yield break;
    }
}
