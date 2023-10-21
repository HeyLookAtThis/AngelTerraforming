using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Volcano : MonoBehaviour
{
    [SerializeField] private Color _iceColor;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private FreezEffect _freezEffect;

    private float _colorChangeSpeed;
    private Ground _ground;
    private bool _isFrozen;

    private Coroutine _heatGenerator;
    private Coroutine _freezer;

    private void Start()
    {
        _colorChangeSpeed = 2;
    }

    public void Initialize(Ground ground)
    {
        _ground = ground;
        _isFrozen = false;
        BeginGenerateGeat();
    }

    public void Freeze()
    {
        if( _isFrozen == false)
        {
            _isFrozen = true;

            _smoke.Stop();
            _freezEffect.Play();

            BeginFreeze();
        }
    }
    
    public void BeginFreeze()
    {
        if (_freezer != null)
            StopCoroutine(_freezer);

        _freezer = StartCoroutine(Freezer());
    }

    private void BeginGenerateGeat()
    {
        if (_heatGenerator != null)
            StopCoroutine(_heatGenerator);

        _heatGenerator = StartCoroutine(HeatGenerator());
    }

    private IEnumerator HeatGenerator()
    {
        float seconds = 0.1f;
        var waitTime = new WaitForSecondsRealtime(seconds);

        while(_isFrozen == false)
        {
            _ground.AddTemperature(seconds);
            yield return waitTime;
        }

        if(_isFrozen)
            yield break;
    }

    private IEnumerator Freezer()
    {
        float seconds = 0.2f;
        var waitTime = new WaitForSecondsRealtime(seconds);

        while (_renderer.material.color != _iceColor)
        {
            _renderer.material.DOColor(_iceColor, seconds * _colorChangeSpeed);
            yield return waitTime;
        }

        if(_renderer.material.color == _iceColor)
            yield break;
    }
}
