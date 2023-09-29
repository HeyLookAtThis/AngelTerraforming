using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;
    [SerializeField] private GrassPainter _painter;

    private bool _hasGrassAround = false;

    private bool _isTrunkEmpty => _hasGrassAround ? false : true;

    public bool HasGrassAround => _hasGrassAround;

    private void Start()
    {
        ChangeLeaves();
    }

    public void GrowGrassAround()
    {
        _painter.Activate();
        _hasGrassAround = true;
        ChangeLeaves();

        _painter.Deactivate();
    }

    private void ChangeLeaves()
    {
        foreach (var tunk in _emptyTunks)
            tunk.SetActive(_isTrunkEmpty);

        foreach (var tunk in _greenTrunks)
            tunk.SetActive(_hasGrassAround);
    }
}
