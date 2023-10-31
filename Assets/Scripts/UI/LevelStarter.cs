using UnityEngine;
using UnityEngine.Events;

public class LevelStarter : MonoBehaviour
{
    private UnityAction _beginning;

    public event UnityAction Beginning
    {
        add => _beginning += value;
        remove => _beginning -= value;
    }

    public void Begin()
    {
        _beginning?.Invoke();
    }
}
