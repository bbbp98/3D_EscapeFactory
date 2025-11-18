using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStar : ItemBase
{
    protected override void OnGetEffect(PlayerCondition player)
    {
        // score up
        //ScoreManager.Instance.AddScore(data.value);
        //Debug.Log(ScoreManager.Instance.GetCurScore());
    }
}
