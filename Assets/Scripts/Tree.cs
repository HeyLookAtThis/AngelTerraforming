using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GrassPainter _painter;
    [SerializeField] private int _radius;
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;

    private bool _hasGrassAround = false;

    private bool _isGrassGrowDelay => false;

    private bool _isTrunkEmpty => _hasGrassAround ? false : true;

    private void Start()
    {
        ChangeLeaves();
    }

    public void GrowGrassAround()
    {
        _painter.BeginFillCell(transform.position, _radius, _isGrassGrowDelay);
        _hasGrassAround = true;
        ChangeLeaves();
    }

    private void ChangeLeaves()
    {
        foreach (var tunk in _emptyTunks)
            tunk.SetActive(_isTrunkEmpty);

        foreach (var tunk in _greenTrunks)
            tunk.SetActive(_hasGrassAround);
    }
}
