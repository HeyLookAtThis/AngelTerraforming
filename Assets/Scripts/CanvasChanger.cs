using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private VolcanoDisplayer _volcanoDisplayer;

    private void OnEnable()
    {
        _volcanoDisplayer.Fulled += ActivateMenuCanvas;
    }

    private void OnDisable()
    {
        _volcanoDisplayer.Fulled -= ActivateMenuCanvas;
    }

    public void ActivateGameCanvas()
    {
        _gameCanvas.enabled = true;
        _menuCanvas.enabled = false;
    }

    public void ActivateMenuCanvas()
    {
        _gameCanvas.enabled = false;
        _menuCanvas.enabled = true;
    }
}
