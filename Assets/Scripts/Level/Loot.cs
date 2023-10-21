using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(AudioSource))]
public abstract class Loot : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _chaseSpeed;

    private SphereCollider _sphereCollider;
    private AudioSource _audioSourse;
    private Coroutine _playerChaser;

    private bool _used; 

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _audioSourse = GetComponent<AudioSource>();
        _effect.Stop();
    }

    public abstract void GiveReward(Player player);

    public void Initialize()
    {
        _model.SetActive(false);
        _used = false;
        _audioSourse.clip = _sound;
        _audioSourse.playOnAwake = false;
        _audioSourse.loop = false;
        _sphereCollider.isTrigger = true;
    }

    public void TurnOn()
    {
        if (_model.activeSelf == false && _used == false)
        {
            _model.SetActive(true);
            _effect.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_model.activeSelf)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                ChaseThePlayer(player.transform.position);
                GiveReward(player);
            }
        }
    }

    private void TurnOff()
    {
        _audioSourse?.Play();
        _model.SetActive(false);
        _effect.Stop();
        _used = true;
    }

    private void ChaseThePlayer(Vector3 target)
    {
        if (_playerChaser != null)
            StopCoroutine(_playerChaser);

        _playerChaser = StartCoroutine(PlayerChaser(target));
    }

    private IEnumerator PlayerChaser(Vector3 target)
    {
        float seconds = 0.01f;
        var waitTime = new WaitForSeconds(seconds);

        while (transform.position != target)
        {
            transform.DOMove(target, seconds * _chaseSpeed);
            yield return waitTime;
        }

        if (transform.position == target)
        {
            TurnOff();
            yield break;
        }
    }
}
