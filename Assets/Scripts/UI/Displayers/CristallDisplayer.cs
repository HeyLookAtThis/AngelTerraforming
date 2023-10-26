public class CristallDisplayer : LootDisplayer
{
    private void OnEnable()
    {
        Player.CristallsNumberChanged += ChangeValue;
    }

    private void OnDisable()
    {
        Player.CristallsNumberChanged -= ChangeValue;
    }
}
