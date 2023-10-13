using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlowersCreator : Instantiator
{
    [SerializeField] private uint _count;
    [SerializeField] private List<Flower> _prefabs;

    protected override void Create()
    {
        uint count = _count;
        float rayOriginHeingt = 1f;

        while (count > 0)
        {
            foreach (var flower in _prefabs)
            {
                Vector3 flowerPosition = Grid.GetRandomCoordinate();

                if (IsEmptyGround(flowerPosition, rayOriginHeingt))
                {
                    Instantiate(flower, flowerPosition, Quaternion.identity, Container);
                    count--;
                }

                if (count == 0)
                    break;
            }
        }
    }
}
