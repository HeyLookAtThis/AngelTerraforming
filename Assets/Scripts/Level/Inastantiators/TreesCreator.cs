using System.Collections.Generic;
using UnityEngine;

public class TreesCreator : Instantiator
{
    [SerializeField] private uint _count;
    [SerializeField] private List<Tree> _prefabs;

    protected override void Create()
    {
        uint count = _count;

        while (count > 0)
        {
            foreach (var tree in _prefabs)
            {
                Vector3 treePosition = GetRandomCoordinate();
                Instantiate(tree, treePosition, Quaternion.identity, Container);

                AddObjectCoordinate(treePosition);

                count--;

                if (count == 0)
                    break;
            }
        }
    }
}
