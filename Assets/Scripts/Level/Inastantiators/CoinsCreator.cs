using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCreator : Instantiator
{
    [SerializeField] private Coin _coin;
    [SerializeField] private uint _amount;

    private void Start()
    {
        Create();
    }

    public override void Create()
    {
        float rayOriginHeingt = 0.2f;

        while(_amount > 0)
        {
            Vector3 position = new Vector3(Random.Range(Grid.Start.x, Grid.End.x), Grid.Start.y, Random.Range(Grid.Start.z, Grid.End.z));

            if (IsEmptyGround(position, rayOriginHeingt))
            {
                Instantiate(_coin, position, Quaternion.identity, Container).Initialize();
                _amount--;
            }
        }
    }
}
