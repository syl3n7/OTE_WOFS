using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    private float bulletSpeed = 110;
    private void Start()
    {
        transform.SetParent(GameObject.Find("PlayableArea").transform);
        Destroy(gameObject, 15f);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            other.gameObject.GetComponent<enemyMovement>().TakeDamage(30); // takes 1 hitpoint
            Destroy(gameObject);
        }
    }
}
