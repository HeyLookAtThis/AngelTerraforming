using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Tree : MonoBehaviour
{
    [SerializeField] private TilemapPainter _painter;
    [SerializeField] private Cloud _cloud;
    [SerializeField] private float _radius;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


}
