using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCreator : Instantiator
{
    [SerializeField] private Coin _coin;
    [SerializeField] private uint _amount;

    private List<Coin> _coins = new List<Coin>();

    public override void Create()
    {
        float rayOriginHeingt = 0.2f;

        ClearLevel();

        while(_amount > 0)
        {
            Vector3 position = new Vector3(Random.Range(Grid.Start.x, Grid.End.x), Grid.Start.y, Random.Range(Grid.Start.z, Grid.End.z));

            if (IsEmptyGround(position, rayOriginHeingt))
            {
                Coin coin = Instantiate(_coin, position, Quaternion.identity, Container);
                coin.Initialize();
                _coins.Add(coin);
                _amount--;
            }
        }
    }

    private void ClearLevel()
    {
        if (_coins != null)
        {
            foreach (Coin coin in _coins)
                coin.Destroy();

            _coins.Clear();
        }
    }

}
