using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private uint _coinsNumber;
    private uint _iceCristallNumber;

    private PlayerMovement _movment;

    public uint CoinsNumber => _coinsNumber;

    public bool HaveCristall => _iceCristallNumber > 0;

    public PlayerMovement Movement => _movment;

    private void Awake()
    {
        _movment = GetComponent<PlayerMovement>();
    }

    public void AddCoin()
    {
        _coinsNumber++;
    }

    public void RemoveCoin()
    {
        _coinsNumber--;
    }

    public void AddIceCristall()
    {
        _iceCristallNumber++;
    }

    public void RemoveIceCristall()
    {
        if (HaveCristall)
            _iceCristallNumber--;
    }
}
