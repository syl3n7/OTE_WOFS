using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    private float enemySpeed = 50;

    [SerializeField] GameObject[] enemyPrefab = new GameObject[4];

    List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        // Start the SpawnEnemies coroutine
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Only spawn a new enemy if there are less than 20 active enemies
            if (activeEnemies.Count < 20)
            {
                // Generate a random number between 0 and 6
                int randomEnemy = Random.Range(0, 7);

                // Instantiate the enemy at a random position on the top of the screen
                GameObject newEnemy = Instantiate(enemyPrefab[randomEnemy], new Vector3(Random.Range(0, Screen.width), Screen.height, 0), Quaternion.identity);

                // Set the enemy's parent to the Enemies game object
                newEnemy.transform.SetParent(GameObject.Find("Enemies_Spawner").transform);

                // Set the enemy's speed to a random number 
                enemySpeed = Random.Range(50, 150);

                // Add the enemy to the activeEnemies list
                activeEnemies.Add(newEnemy);
            }

            // Wait for 1 second before spawning the next enemy
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        foreach (var enemy in activeEnemies)
        {
            // Move the enemy downwards
            enemy.transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
        }

        if (activeEnemies.Count >= 10)
        {
            foreach (var enemy in activeEnemies.ToList())
            {
                // Check if the enemy is below y = 0
                if (enemy.transform.position.y < 0)
                {
                    // Destroy the enemy
                    Destroy(enemy);
                    activeEnemies.Remove(enemy);
                }
            }
        }
    }
}
