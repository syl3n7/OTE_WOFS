using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    private float bulletSpeed = 128;
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.fixedDeltaTime);
    }
}
