using System.Collections;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    [SerializeField] private Material _coldMaterial;
    [SerializeField] private MeshRenderer _renderer;

    private Ground _ground;
    private bool _isFrozen;

    private Coroutine _heatGenerator;

    private void Start()
    {
        BeginGenerateHeat();
    }

    public void Initialize(Ground ground)
    {
        _ground = ground;
    }

    public void Freeze()
    {
        _isFrozen = true;
        _renderer.material = _coldMaterial;
    }
    
    public void BeginGenerateHeat()
    {
        if( _heatGenerator != null )
            StopCoroutine(_heatGenerator);

        _heatGenerator = StartCoroutine(HeatGenerator());
    }

    private IEnumerator HeatGenerator()
    {
        float seconds = 0.1f;
        var waitTime = new WaitForSecondsRealtime(seconds);
        Debug.Log(this);

        while(_isFrozen == false)
        {
            _ground.AddTemperature(seconds);
            yield return waitTime;
        }

        if(_isFrozen)
            yield break;
    }
}
