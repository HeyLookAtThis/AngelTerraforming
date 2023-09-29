using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Reservoir : MonoBehaviour
{
    [SerializeField] private float _capacity;

    private float _currentStock;
    private bool _isEmpty;    
    
    public bool IsEmpty => _isEmpty;

    public float ChangeStepsCount => _capacity / _changeVolume;

    private float _emptyStock => 0f;

    private float _changeVolume => 0.1f;

    private UnityAction _waterIsOver;

    public event UnityAction WaterIsOver
    {
        add => _waterIsOver += value;
        remove => _waterIsOver -= value;
    }    

    private void Start()
    {
        _currentStock = _capacity;
    }

    public void ReplenishWater()
    {
        if (_currentStock < _capacity)
        {
            ChangeStock(_capacity);
            _isEmpty = false;
        }
    }

    public void PourWater()
    {
        if (_currentStock > _emptyStock)
        {
            ChangeStock(_emptyStock);
        }
        else
        {
            _isEmpty = true;
            _waterIsOver?.Invoke();
        }
    }

    private void ChangeStock(float targetStock)
    {
        _currentStock = Mathf.MoveTowards(_currentStock, targetStock, _changeVolume);
    }
}
