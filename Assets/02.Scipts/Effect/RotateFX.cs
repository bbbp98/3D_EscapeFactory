using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFX : MonoBehaviour
{
    [SerializeField] private float speed = 60f;

    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
