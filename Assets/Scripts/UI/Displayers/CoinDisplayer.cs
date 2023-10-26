public class CoinDisplayer : LootDisplayer
{
    private void OnEnable()
    {
        Player.CoinsNumberChanged += ChangeValue;
    }

    private void OnDisable()
    {
        Player.CoinsNumberChanged -= ChangeValue;
    }
}
