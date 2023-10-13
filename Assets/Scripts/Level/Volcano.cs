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
        var waitTime = new WaitForEndOfFrame();

        while(_isFrozen == false)
        {
            _ground.AddTemperature(Time.deltaTime);
            yield return waitTime;
        }

        if(_isFrozen)
            yield break;
    }
}
