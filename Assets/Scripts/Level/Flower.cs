using UnityEngine;

public class Flower : Plant
{
    private GameObject _model;

    private void Awake()
    {
        _model = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        TurnOffVisible();
    }

    public override void MakeGreen()
    {
        if (IsGreen == false)
        {
            _model.SetActive(true);
            SetGreen();
        }
    }

    private void TurnOffVisible()
    {
        _model.SetActive(false);
    }
}
