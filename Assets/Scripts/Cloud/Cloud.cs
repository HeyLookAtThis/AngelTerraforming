using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private float _level;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _targetPlace;

    public Player Player => _player;

    public Transform TargetPlace => _targetPlace;

    public float Level => _level;
}
