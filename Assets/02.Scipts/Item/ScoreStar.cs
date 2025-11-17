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

    protected override void OnGetEffect()
    {
        // score up
    }
}
