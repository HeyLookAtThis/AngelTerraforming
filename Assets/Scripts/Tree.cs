using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private TilemapPainter _painter;
    [SerializeField] private int _radius;
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;

    private bool _isGrassGrowDelay => false;

    private void Start()
    {
        CustomizeLeaves();
    }

    public void GrowGrassAround()
    {
        if (_painter.CanGrowGrass(transform.position))
        {
            _painter.OnBeginFillCell(_radius, _isGrassGrowDelay);
            CustomizeLeaves();
        }
    }

    private void CustomizeLeaves()
    {
        if (_painter.CanGrowGrass(transform.position))
        {
            foreach (var tunk in _emptyTunks)
                tunk.SetActive(false);

            foreach(var tunk in _greenTrunks)
                tunk.SetActive(true);
        }
        else
        {
            foreach (var tunk in _emptyTunks)
                tunk.SetActive(true);

            foreach (var tunk in _greenTrunks)
                tunk.SetActive(false);
        }
    }
}
