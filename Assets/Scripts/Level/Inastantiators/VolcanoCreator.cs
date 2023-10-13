using System.Collections.Generic;
using UnityEngine;

public class VolcanoCreator : Instantiator
{
    [SerializeField] private uint _count;
    [SerializeField] private List<Volcano> _prefabs;

    public uint Count => _count;

    protected override void Create()
    {
        uint count = _count;

        while (count > 0)
        {
            foreach (var tree in _prefabs)
            {
                Vector3 volcanoPosition = GetRandomCoordinate();

                Instantiate(tree, volcanoPosition, Quaternion.identity, Container).Initialize(Ground);

                AddObjectCoordinate(volcanoPosition);

                count--;

                if (count == 0)
                    break;
            }
        }
    }
}
