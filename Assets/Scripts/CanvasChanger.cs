using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _menuCanvas;

    public void ActivateGameCamera()
    {
        _menuCanvas.enabled = false;
        _gameCanvas.enabled = true;
    }

    public void ActivateMenuCamera()
    {
        _gameCanvas.enabled = false;
        _menuCanvas.enabled = true;
    }

}
