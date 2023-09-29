using System.Collections;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private SphereCollider _sphereCollider;
    private Coroutine _growBeginner;
    private float _duration = 0.2f;

    public bool Growed { get; private set; }

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        //_sphereCollider.isTrigger = true;
    }

    public void Grow()
    {
        _particleSystem.gameObject.SetActive(true);
        BeginToGrow();
        Growed = true;
    }

    public void TurnOff()
    {
        Growed = false;
        _particleSystem.gameObject.SetActive(false);
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
