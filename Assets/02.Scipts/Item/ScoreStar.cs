using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStar : MonoBehaviour
{
    [SerializeField] private ItemData data;
    [SerializeField] private float rotateSpeed = 100f;

    void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        // get score
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
