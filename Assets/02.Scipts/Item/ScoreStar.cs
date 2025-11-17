using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStar : ItemBase
{
    [SerializeField] private float rotateSpeed = 100f;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    protected override void OnGetEffect(PlayerCondition player)
    {
        // score up
        //ScoreManager.Instance.AddScore(data.value);
        //Debug.Log(ScoreManager.Instance.GetCurScore());
    }
}
