using UnityEngine;

public class Flower : MonoBehaviour
{
    private GameObject _model;

    public bool IsVisible { get; private set; }

    private void Awake()
    {
        _model = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        TurnOffVisible();
    }

    public void TurnOnVisible()
    {
        _model.SetActive(true);
        IsVisible = true;
    }

    private void TurnOffVisible()
    {
        _model.SetActive(false);
        IsVisible = false;
    }
}
