using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    public Player Player => _player;

    public PlayerMovement PlayerMovement => _playerMovement;
}
