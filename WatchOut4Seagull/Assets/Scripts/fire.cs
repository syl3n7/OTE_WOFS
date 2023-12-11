using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public Transform firePoint;

    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        while (true)
        {
            // Instantiate bulletPrefab1 or bulletPrefab2 randomly
            int rnd = UnityEngine.Random.Range(1, 3);
            if (rnd == 1)
            {
                Instantiate(bulletPrefab1, firePoint.position, firePoint.rotation);
            }
            else
            {
                Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
            }

            // Wait for 0.7 seconds before spawning the next bullet
            yield return new WaitForSeconds(0.7f);
        }
    }
}