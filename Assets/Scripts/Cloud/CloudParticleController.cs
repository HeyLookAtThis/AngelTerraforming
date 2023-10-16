using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Reservoir), typeof(Scanner))]
public class CloudParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainParticleSystem;
    [SerializeField] private ParticleSystem _waterCollecterParticleSystem;

    private Reservoir _reservoir;
    private Scanner _scanner;

    private float _previousValue;

    private void Awake()
    {
        _reservoir = GetComponent<Reservoir>();
        _scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        _reservoir.WaterIsOver += OnTryStopRain;
        _reservoir.FilledUp += OnTryStopCollecter;

        _scanner.FoundDryPlant += OnTryBeginPlayRain;
        _scanner.FoundWater += OnTryStopRain;

        _scanner.FoundWater += OnTryBeginPlayCollecter;
        _scanner.LostWater += OnTryStopCollecter;
    }

    private void OnDisable()
    {
        _reservoir.FilledUp -= OnTryStopCollecter;
        _reservoir.WaterIsOver -= OnTryStopRain;

        _scanner.FoundDryPlant -= OnTryBeginPlayRain;
        _scanner.FoundWater -= OnTryStopRain;

        _scanner.FoundWater -= OnTryBeginPlayCollecter;
        _scanner.LostWater -= OnTryStopCollecter;
    }

    private void Start()
    {
        _waterCollecterParticleSystem.Stop();
        _rainParticleSystem.Stop();
    }

    private void OnTryBeginPlayRain()
    {
        if (_rainParticleSystem.isPlaying == false)
            _rainParticleSystem.Play();
    }

    private void OnTryBeginPlayCollecter()
    {
        if(_waterCollecterParticleSystem.isPlaying == false)
            _waterCollecterParticleSystem.Play();
    }

    private void OnTryStopRain()
    {
        if (_rainParticleSystem.isPlaying)
            _rainParticleSystem.Stop();
    }

    private void OnTryStopCollecter()
    {
        if (_waterCollecterParticleSystem.isPlaying)
            _waterCollecterParticleSystem.Stop();
    }
}
