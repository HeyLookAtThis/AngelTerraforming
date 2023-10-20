using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.2f;

    private ParticleSystem _particleSystem;
    private Coroutine _starter;
    private AudioSource _audio;
    private Light _light;
    private float _intensity;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        _light = GetComponent<Light>();
        _intensity = _light.intensity;
        _light.intensity = 0;
    }

    public void Play()
    {
        _audio.Play();
        _light.intensity = _intensity;
        _particleSystem.Play();

        if (_starter != null)
            StopCoroutine(_starter);

        _starter = StartCoroutine(Starter());
    }

    private IEnumerator Starter()
    {
        float seconds = _lifeTime * _lifeTime;
        var waitTime = new WaitForSeconds(seconds);
        float passedTime = 0;

        while (passedTime < _lifeTime)
        {
            _light.intensity -= _intensity * (seconds / _lifeTime);
            passedTime += seconds;
            yield return waitTime;
        }

        if (passedTime >= _lifeTime)
        {
            _light.intensity = 0;
            yield break;
        }
    }
}
