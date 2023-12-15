using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    private float bulletSpeed = 90;

    void Start()
    {
        Destroy(gameObject, 15f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(10); // takes 1 hitpoint
            Destroy(gameObject);
        }
    }
}
