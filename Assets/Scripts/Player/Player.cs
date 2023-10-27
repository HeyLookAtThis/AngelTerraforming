using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _coinsNumber;
    private int _cristallNumber;

    private PlayerMovement _movment;

    private UnityAction<int> _cristallsNumberChanged;
    private UnityAction<int> _coinsNumberChanged;

    public event UnityAction<int> CristallsNumberChanged
    {
        add => _cristallsNumberChanged += value;
        remove => _cristallsNumberChanged -= value;
    }

    public event UnityAction<int> CoinsNumberChanged
    {
        add => _coinsNumberChanged += value;
        remove => _coinsNumberChanged -= value;
    }

    public int CoinsNumber => _coinsNumber;

    public bool HaveCristall => _cristallNumber > 0;

    public PlayerMovement Movement => _movment;

    private void Awake()
    {
        _movment = GetComponent<PlayerMovement>();
    }

    public void AddCoin()
    {
        _coinsNumber++;
        _coinsNumberChanged?.Invoke(_coinsNumber);
    }

    public void RemoveCoin()
    {
        if(_coinsNumber > 0)
        {
            _coinsNumber--;
            _coinsNumberChanged?.Invoke(_coinsNumber);
        }
    }

    public void AddCristall()
    {
        _cristallNumber++;
        _cristallsNumberChanged?.Invoke(_cristallNumber);
    }

    public void RemoveIceCristall()
    {
        if (HaveCristall)
        {
            _cristallNumber--;
            _cristallsNumberChanged?.Invoke(_cristallNumber);
        }
    }
}
