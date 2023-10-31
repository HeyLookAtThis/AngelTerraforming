using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _targetPlace;
    [SerializeField] private LevelStarter _levelSarter;
    [SerializeField] private LevelFinisher _levelFinisher;

    public Player Player => _player;

    public Transform TargetPlace => _targetPlace;

    public LevelStarter LevelStarter => _levelSarter;

    public LevelFinisher LevelFinisher => _levelFinisher;
}
