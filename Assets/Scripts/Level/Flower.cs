using UnityEngine;

public class Flower : MonoBehaviour
{
    private GameObject _model;

    private void Awake()
    {
        _model = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        _model.SetActive(false);
    }

    public void TurnOnVisible()
    {
        _model.SetActive(true);
    }
}
