using TMPro;
using UnityEngine;

public class StartGameButton : MenuButton
{
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private CanvasChanger _canvasChanger;
    [SerializeField] private string _text;

    [SerializeField] private RewardsPanel _rewardsPanel;

    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private LevelCounter _levelCounter;
    [SerializeField] private PlayerMovement _playerMovement;

    public override void Initialize()
    {
        base.Initialize();
        _textMeshPro.SetText(_text);
        AddAction(_levelCounter.SetNextLevel);
        AddAction(TurnOnGameCamera);
        AddAction(_rewardsPanel.TurnOff);
        AddAction(_playerMovement.SetStartingPosition);
    }

    public override void Unsubscrube()
    {
        base.Unsubscrube();
        RemoveAction(_levelCounter.SetNextLevel);
        RemoveAction(TurnOnGameCamera);
        RemoveAction(_rewardsPanel.TurnOff);
        RemoveAction(_playerMovement.SetStartingPosition);
    }

    private void TurnOnGameCamera()
    {
        _cameraChanger.ActivateGameCamera();
        _canvasChanger.ActivateGameCanvas();
    }
}
