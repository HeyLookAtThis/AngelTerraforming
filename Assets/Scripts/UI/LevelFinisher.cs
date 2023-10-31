using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelFinisher : MonoBehaviour
{
    [SerializeField] private VolcanoDisplayer _volcanoDisplayer;

    private UnityAction _begun;

    public event UnityAction Begun
    {
        add => _begun += value;
        remove => _begun -= value;
    }

    private void OnEnable()
    {
        _volcanoDisplayer.Fulled += Begin;
    }

    private void OnDisable()
    {
        _volcanoDisplayer.Fulled -= Begin;
    }

    private void Begin()
    {
        _begun?.Invoke();
    }
}
