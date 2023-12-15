using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public int enemyHealth = 90;
    public GameObject bulletPrefab;
    private float enemySpeed = 40;
    void Update()
    {
        // Move the enemy downwards
        gameObject.transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (gameObject.transform.position.y < 70)
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
        // Check if the enemy's health is below or equal to 0
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator BulletFiring()
    {
        while (true)
        {
            // Instantiate a bullet at the enemy's position
            GameObject newBullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);

            //set the bullet's parent to the enemy's game object
            newBullet.transform.SetParent(gameObject.transform);

            // Wait for a random interval between bulletFireIntervalMin and bulletFireIntervalMax
            yield return new WaitForSeconds(5f);
        }
    }
    public void TakeDamage(int f)
    {
        StartCoroutine(DelayedDamage(f));
    }

    private IEnumerator DelayedDamage(int f)
    {
        enemyHealth -= f;
        Debug.Log("Took Damage from player");
        if (enemyHealth <= 0)
        {
            //get the player object and set the highScore
            GameObject.Find("player").GetComponent<PlayerController>().highScore += 100;
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
    }
}
