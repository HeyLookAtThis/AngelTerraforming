using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
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

    public bool CanGrowGrass(Vector3 point)
    {
        _fieldPosition = _tilemap.WorldToCell(point);

        if (_tilemap.GetTile(_fieldPosition) != _tileBase)
            return true;

        return false;    
    }

    public void OnBeginFillCell(int radius, bool isDeferred)
    {
        if (_cellFiller != null)
            StopCoroutine(_cellFiller);

        _cellFiller = StartCoroutine(CellFiller(radius, isDeferred));        
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

    private IEnumerator CellFiller(int radius, bool isDeferred)
    {
        var waitTime = new WaitForEndOfFrame();

        TryFillCell(_fieldPosition);

        for (int i = -radius; i <= radius; i++)
        {
            for (int j = radius; j >= -radius; j--)
            {
                Vector3Int neighborFieldPosition = new Vector3Int(_fieldPosition.x + i, _fieldPosition.y + j, _fieldPosition.z);

                if (Vector3Int.Distance(_fieldPosition, neighborFieldPosition) <= radius)
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
