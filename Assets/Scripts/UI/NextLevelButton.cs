using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private LevelGenerator _generator;
    [SerializeField] private VolcanoDisplayer _volcanoDisplayer;
    [SerializeField] private StartGameButton _startGameButton;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _startGameButton.AddAction(TurnOff);
        _button.onClick?.AddListener(_generator.SetNextLevel);
        _button.onClick?.AddListener(_generator.GenerateLevel);
        _volcanoDisplayer.Fulled += TurnOn;
    }

    private void OnDisable()
    {
        _startGameButton.RemoveAction(TurnOff);
        _button.onClick?.RemoveListener(_generator.SetNextLevel);
        _button.onClick?.RemoveListener(_generator.GenerateLevel);
        _volcanoDisplayer.Fulled -= TurnOn;
    }

    private void TurnOff()
    {
        _image.enabled = false;
        _button.enabled = false;
    }

    private void TurnOn()
    {
        _image.enabled = true;
        _button.enabled = true;
    }
}
