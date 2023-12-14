using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    private float bulletSpeed = 100;

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.fixedDeltaTime);
        Destroy(gameObject, 30f);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Player hit");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(10); // takes 1 hitpoint
            Destroy(gameObject);
        }
    }
}
