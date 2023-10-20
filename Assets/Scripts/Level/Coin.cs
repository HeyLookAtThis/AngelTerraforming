public class Coin : Loot
{
    public override void GiveReward(Player player)
    {
        player.AddCoin();
    }
}
