using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GrassPainter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tileBase;

    private Coroutine _cellFiller;

    private UnityAction _fieldFilled;

    public event UnityAction FieldFilled
    {
        add => _fieldFilled += value;
        remove => _fieldFilled -= value;
    }

    public bool CanFillCell(Vector3 worldCellPosition)
    {
        Vector3Int cellPosition = _tilemap.WorldToCell(worldCellPosition);

        if (_tilemap.GetTile(cellPosition) != _tileBase && IsThisGround(cellPosition))
            return true;

        return false;
    }

    public void BeginFillCell(Vector3 cellCenter, int radius, bool isDeferred)
    {
        if (_cellFiller != null)
            StopCoroutine(_cellFiller);

        _cellFiller = StartCoroutine(CellFiller(cellCenter, radius, isDeferred));
    }

    private void TryFillCell(Vector3Int position)
    {
        if (_tilemap.GetTile(position) != _tileBase && IsThisGround(position))
        {
            _tilemap.BoxFill(position, _tileBase, position.x, position.y, position.x, position.y);
            _fieldFilled?.Invoke();
        }
    }

    private bool IsThisGround(Vector3Int cell)
    {
        Vector3 worldPosition = _tilemap.CellToWorld(cell);

        Physics.Raycast(worldPosition, Vector3.down, out RaycastHit hit);

        if(hit.collider.TryGetComponent<Ground>(out Ground ground))
            return true;

        return false;
    }

    private IEnumerator CellFiller(Vector3 cellWorldPosition ,int radius, bool isDeferred)
    {
        var waitTime = new WaitForEndOfFrame();

        Vector3Int cellPosition = _tilemap.WorldToCell(cellWorldPosition);

        TryFillCell(cellPosition);

        for (int i = -radius; i <= radius; i++)
        {
            for (int j = radius; j >= -radius; j--)
            {
                Vector3Int neighborFieldPosition = new Vector3Int(cellPosition.x + i, cellPosition.y + j, cellPosition.z);

                if (Vector3Int.Distance(cellPosition, neighborFieldPosition) <= radius)
                {
                    TryFillCell(neighborFieldPosition);

                    if (isDeferred)
                        yield return waitTime;
                }
            }

            if (i > radius)
                yield break;
        }
    }
}
