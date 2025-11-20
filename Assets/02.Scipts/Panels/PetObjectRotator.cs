using UnityEngine;

public class PetObjectRotator : MonoBehaviour
{
    float rotateY = 0f;

    void FixedUpdate()
    {
        rotateY += Time.fixedDeltaTime * 50f;

        if(rotateY > 360f) rotateY -= 360f;
        transform.rotation = Quaternion.Euler(0f, rotateY , 0f);
    }
}
