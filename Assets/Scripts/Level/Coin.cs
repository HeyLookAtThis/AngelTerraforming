using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private GameObject _model;
    [SerializeField] private ParticleSystem _effect;

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
        if(_model.activeSelf)
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
