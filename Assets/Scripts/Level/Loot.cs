using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(AudioSource))]
public abstract class Loot : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private GameObject _model;

    private SphereCollider _sphereCollider;
    private AudioSource _audioSourse;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _audioSourse = GetComponent<AudioSource>();
    }

    public void Initialize()
    {
        _effect.Stop();
        _model.SetActive(false);
        _audioSourse.clip = _sound;
        _audioSourse.playOnAwake = false;
        _audioSourse.loop = false;
        _sphereCollider.isTrigger = true;
    }

    public void TurnOn()
    {
        if (_model.activeSelf == false)
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
                _audioSourse?.Play();
                _model.SetActive(false);
                _effect.Stop();
            }
        }
    }
}
