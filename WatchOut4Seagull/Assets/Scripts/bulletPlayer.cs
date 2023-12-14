using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    private float bulletSpeed = 100;

    private void Start()
    {
        transform.SetParent(GameObject.Find("PlayableArea").transform);
        Destroy(gameObject, 30f);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Player hit");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(30); // takes 1 hitpoint
            Destroy(gameObject);
        }
    }
}
