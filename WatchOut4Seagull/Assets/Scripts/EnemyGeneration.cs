using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    public int enemyHealth = 90;

    [SerializeField] GameObject[] enemyPrefab = new GameObject[4];
    [SerializeField] GameObject bulletPrefab;

    void Start()
    {
        // Start the SpawnEnemies coroutine
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Generate a random number between 0 and 6
            int randomEnemy = Random.Range(0, 4);

            // Instantiate the enemy at a random position on the top of the screen
            GameObject newEnemy = Instantiate(enemyPrefab[randomEnemy], new Vector3(Random.Range(140, Screen.width - 140), Screen.height + 170, 0), Quaternion.identity);

            // Set the enemy's parent to the Enemies game object
            newEnemy.transform.SetParent(GameObject.Find("Enemy_Spawner").transform);

            // Start the BulletFiring coroutine for the new enemy
            StartCoroutine(newEnemy.GetComponent<enemyMovement>().BulletFiring());

            // Wait for 1 second before spawning the next enemy
            yield return new WaitForSeconds(5f);
        }
    }
}
