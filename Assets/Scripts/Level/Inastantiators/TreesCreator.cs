using System.Collections.Generic;
using UnityEngine;

public class TreesCreator : Instantiator
{
    [SerializeField] private uint _count;
    [SerializeField] private List<Tree> _prefabs;
    [SerializeField] private IceCristallCreator _creator;

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

                if(_creator.Count > 0)
                {
                    float rayHeight = 0.5f;
                    Vector3 cristallPosition = _creator.GetRandomPosition(treePosition);

                    while (IsEmptyGround(cristallPosition, rayHeight) == false)
                        cristallPosition = _creator.GetRandomPosition(treePosition);

                    _creator.Create(cristallPosition);
                }

                if (count == 0)
                    break;
            }
        }
    }
}
