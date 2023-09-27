using System.Collections;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private Coroutine _growBeginner;
    private float _duration = 1f;

    public void Grow()
    {
        BeginToGrow();
    }

    private void BeginToGrow()
    {
        if (_growBeginner != null)
            StopCoroutine(_growBeginner);

        _growBeginner = StartCoroutine(GrowBeginner());
    }

    private IEnumerator GrowBeginner()
    {
        var WaitTime = new WaitForEndOfFrame();
        float passedTime = 0;

        _particleSystem.Play();

        while(passedTime < _duration)
        {
            passedTime += Time.deltaTime;
            yield return WaitTime;
        }

        if(passedTime >= _duration)
        {
            _particleSystem.Pause();
            yield break;
        }
    }
}
