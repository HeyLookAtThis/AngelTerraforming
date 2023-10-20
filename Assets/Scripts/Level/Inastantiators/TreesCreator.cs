using System.Collections.Generic;
using UnityEngine;

public class TreesCreator : Instantiator
{
    [SerializeField] private List<Tree> _prefabs;
    [SerializeField] private CristallCreator _cristallCreator;

    private List<Cristall> _iceCristalls = new List<Cristall>();
    private List<Tree> _trees = new List<Tree>();

    public void ClearLevel()
    {
        if (_trees != null)
        {
            foreach (Tree tree in _trees)
                Destroy(tree);

            foreach (Cristall iceCristall in _iceCristalls)
                Destroy(iceCristall);

            _trees.Clear();
            _iceCristalls.Clear();
        }
    }

    public override void Create()
    {
        int levelCoefficient = 12;
        int multiplier = 2;
        int count = levelCoefficient - LevelGenerator.CurrentLevel * multiplier;
        int cristallCount = count / multiplier;

        while (count > 0)
        {
            foreach (var tree in _prefabs)
            {
                Vector3 treePosition = GetRandomCoordinate();

                _trees.Add(Instantiate(tree, treePosition, Quaternion.identity, Container));

                count--;

                if(cristallCount > 0)
                {
                    float rayHeight = 0.5f;
                    Vector3 cristallPosition = _cristallCreator.GetRandomPosition(treePosition);

                    while (IsEmptyGround(cristallPosition, rayHeight) == false)
                        cristallPosition = _cristallCreator.GetRandomPosition(treePosition);

                    _iceCristalls.Add(_cristallCreator.Create(cristallPosition));

                    cristallCount--;
                }

                if (count == 0)
                    break;
            }
        }
    }
}
