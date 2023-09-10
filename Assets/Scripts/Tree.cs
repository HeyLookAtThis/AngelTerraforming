using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Tree : MonoBehaviour
{
    [SerializeField] private TilemapPainter _painter;
    [SerializeField] private int _radius;

    private bool _isGrassGrowDiferred => false;

    private bool _isGrowGrassAround;

    public void GrowGrassAround()
    {
        if (_isGrowGrassAround == false)
        {
            _painter.OnBeginFillCell(_radius, _isGrassGrowDiferred);
            _isGrowGrassAround = true;
        }
    }
}
