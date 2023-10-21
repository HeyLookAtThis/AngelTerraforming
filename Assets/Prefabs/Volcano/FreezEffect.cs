using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(AudioSource))]
public class FreezEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audio;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _particleSystem.Stop();
    }

    public void Play()
    {
        _audio.Play();
        _particleSystem.Play();
    }
}
