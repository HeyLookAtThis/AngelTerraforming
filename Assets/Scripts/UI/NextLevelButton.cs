using UnityEngine;

public class NextLevelButton : MenuButton
{
    [SerializeField] private StartGameButton _startGameButton;

    public override void Initialize()
    {
        base.Initialize();
        AddAction(_startGameButton.Click);
    }

    public override void Unsubscrube()
    {
        base.Unsubscrube();
        RemoveAction(_startGameButton.Click);
    }
}
