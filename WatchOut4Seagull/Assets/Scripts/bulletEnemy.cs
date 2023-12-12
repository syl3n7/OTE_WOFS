using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    [Range(8, 32)]
    private float bulletSpeed = 64;
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.fixedDeltaTime);
    }
}
