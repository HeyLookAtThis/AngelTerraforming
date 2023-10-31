using System.Collections;
using UnityEngine;

public class Grass : Plant
{
    [SerializeField] private ParticleSystem _particleSystem;

    private Coroutine _growBeginner;
    private float _duration = 0.3f;

    public override void MakeGreen()
    {
        if (IsGreen == false)
        {
            _particleSystem.gameObject.SetActive(true);
            BeginToGrow();
            TurnOnGreen();
        }
    }

    public void TurnOff()
    {
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
        TurnOffGreen();
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
