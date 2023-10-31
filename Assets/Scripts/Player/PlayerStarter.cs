using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStarter : MonoBehaviour
{
    [SerializeField] private LevelStarter _levelStarter;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private Water _water;

    private PlayerMovement _playerMovement;

    private Vector3 _startPosition;
    private float _speed;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _startPosition = new Vector3(_water.transform.position.x, _playerMovement.JumpHeight, _water.transform.position.z);
        _speed = 0.1f;
    }

    private void OnEnable()
    {
        _levelStarter.Beginning += SetStartingPosition;
        _levelFinisher.Begun += SetStartingPosition;
    }

    private void OnDisable()
    {
        _levelStarter.Beginning -= SetStartingPosition;
        _levelFinisher.Begun -= SetStartingPosition;
    }

    public void SetStartingPosition()
    {
        _playerMovement.TurnOffGravity();

        transform.DOMove(_startPosition, _speed);
    }
}