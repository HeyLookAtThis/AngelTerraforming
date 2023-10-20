using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlowersCreator : Instantiator
{
    [SerializeField] private int _count;
    [SerializeField] private List<Flower> _prefabs;

    private void Start()
    {
        Create();
    }

    public override void Create()
    {
        float rayOriginHeingt = 1f;

        while (_count > 0)
        {
            foreach (var flower in _prefabs)
            {
                Vector3 flowerPosition = Grid.GetRandomCoordinate();

                if (IsEmptyGround(flowerPosition, rayOriginHeingt))
                {
                    Instantiate(flower, flowerPosition, Quaternion.identity, Container);
                    _count--;
                }

                if (_count == 0)
                    break;
            }
        }
    }
}
