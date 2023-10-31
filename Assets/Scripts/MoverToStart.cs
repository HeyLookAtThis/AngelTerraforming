using DG.Tweening;
using UnityEngine;

public abstract class MoverToStart : MonoBehaviour
{
    [SerializeField] private LevelStarter _levelStarter;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private Water _water;

    private Vector3 _startPosition;
    private float _speed;

    private void Awake()
    {
        float height = 4;
        _startPosition = new Vector3(_water.transform.position.x, height, _water.transform.position.z);
        _speed = 0.1f;
    }

    private void OnEnable()
    {
        _levelStarter.Beginning += SetPosition;
        _levelFinisher.Begun += SetPosition;
    }

    private void OnDisable()
    {
        _levelStarter.Beginning -= SetPosition;
        _levelFinisher.Begun -= SetPosition;
    }

    public virtual void SetPosition()
    {
        transform.DOMove(_startPosition, _speed);
    }
}
