using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private List<GameObject> _emptyTunks;
    [SerializeField] private List<GameObject> _greenTrunks;

    private bool _hasGrassAround = false;

    private bool _isTrunkEmpty => _hasGrassAround ? false : true;

    public bool HasGrassAround => _hasGrassAround;

    private void Start()
    {
        ChangeLeaves();
    }

    public void MakeGreenAround()
    {
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
