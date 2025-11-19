public class ScoreStar : ItemBase
{
    protected override void OnGetEffect(PlayerCondition player)
    {
        // score up
        ScoreManager.Instance.AddScore(data.value);
    }
}
