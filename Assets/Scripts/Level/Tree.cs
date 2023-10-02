using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    [SerializeField] private float _radius;
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;

    private bool _isTrunkEmpty => !IsGreen;

    public bool HasGrassAround => IsGreen;

    private void Start()
    {
        ChangeLeaves();
    }

    public override void MakeGreen()
    {
        if (IsGreen == false)
        {
            SetGreen();
            ChangeLeaves();
        }    
    }

    private void ChangeLeaves()
    {
        foreach (var tunk in _emptyTunks)
            tunk.SetActive(_isTrunkEmpty);

        foreach (var tunk in _greenTrunks)
            tunk.SetActive(IsGreen);
    }
}
