using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private LevelStarter _levelStarter;

    private void OnEnable()
    {
        _levelFinisher.Begun += ActivateMenuCanvas;
        _levelStarter.Beginning += ActivateGameCanvas;
    }

    private void OnDisable()
    {
        _levelFinisher.Begun -= ActivateMenuCanvas;
        _levelStarter.Beginning -= ActivateGameCanvas;
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
