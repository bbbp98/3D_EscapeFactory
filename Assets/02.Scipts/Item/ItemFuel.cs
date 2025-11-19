public class ItemFuel : ItemBase
{
    protected override void OnGetEffect(PlayerCondition player)
    {
        player.Heal();
    }
}
