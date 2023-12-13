using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    private float bulletSpeed = 128;

    private void Start()
    {
        transform.SetParent(GameObject.Find("PlayableArea").transform);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.fixedDeltaTime);
    }
}
