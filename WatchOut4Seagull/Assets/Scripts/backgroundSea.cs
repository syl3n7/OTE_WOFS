using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class backgroundSea : MonoBehaviour
{

    //generate Sea "blocks" and make so that appears to the player that the sea is moving
    //! the game needs 3 blocks of the sea preset, one spawning, one in the current view and one baing despawned.
    [Range(2, 50)]
    private float backgroundSpeed = 50;
    [SerializeField] GameObject backgroundPrefab;
    List<GameObject> activeBackground = new List<GameObject>();

    void Start()
    {
        // Start the SpawnClouds coroutine
        StartCoroutine(backgroundSpawn());
    }

    IEnumerator backgroundSpawn()
    {
        while (true)
        {
            // Only spawn a new cloud if there are less than 20 active clouds
            if (activeBackground.Count < 3)
            {
                // Instantiate the cloud at a random position
                GameObject newBackground = Instantiate(backgroundPrefab, new Vector3(0, Screen.height + 270, 0), Quaternion.identity);

                // Set the cloud's parent to the Clouds game object
                newBackground.transform.SetParent(GameObject.Find("Background").transform);

                // Add the cloud to the activeClouds list
                activeBackground.Add(newBackground);
            }

            // Wait for 1 second before spawning the next cloud
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        foreach (var background in activeBackground)
        {
            // Move the cloud to the right
            background.transform.Translate(Vector3.right * backgroundSpeed * Time.deltaTime);
        }

        if (activeBackground.Count >= 10)
        {
            foreach (var background in activeBackground.ToList())
            {
                // Check if the cloud is beyond the screen width 
                if (background.transform.position.x > Screen.width)
                {
                    // Destroy the cloud
                    Destroy(background);
                    activeBackground.Remove(background);
                }
            }
        }
    }
}
