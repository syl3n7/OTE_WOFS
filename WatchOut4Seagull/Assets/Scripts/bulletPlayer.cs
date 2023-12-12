using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    private float bulletSpeed = 64;

    private void Start()
    {
        transform.SetParent(GameObject.Find("player").transform);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.fixedDeltaTime);
    }
}
