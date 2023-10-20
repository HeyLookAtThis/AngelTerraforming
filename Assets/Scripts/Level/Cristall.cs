public class Cristall : Loot
{
    private void Start()
    {
        Initialize();
    }

    public override void GiveReward(Player player)
    {
        player.AddIceCristall();
    }
}
