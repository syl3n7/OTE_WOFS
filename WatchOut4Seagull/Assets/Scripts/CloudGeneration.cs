using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class CloudGeneration : MonoBehaviour
{
    private float cloudSpeed = 50;

    [SerializeField] GameObject[] cloudPrefab = new GameObject[7];

    List<GameObject> activeClouds = new List<GameObject>();

    void Start()
    {
        // Start the SpawnClouds coroutine
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        while (true)
        {
            // Only spawn a new cloud if there are less than 20 active clouds
            if (activeClouds.Count < 20)
            {
                // Generate a random number between 0 and 6
                int randomCloud = Random.Range(0, 7);

                // Instantiate the cloud at a random position
                GameObject newCloud = Instantiate(cloudPrefab[randomCloud], new Vector3(-200, Random.Range(0, Screen.height), 0), Quaternion.identity);

                // Set the cloud's parent to the Clouds game object
                newCloud.transform.SetParent(GameObject.Find("Clouds_Spawner").transform);

                // Set the cloud's speed to a random number 
                cloudSpeed = Random.Range(80, 170);

                // Add the cloud to the activeClouds list
                activeClouds.Add(newCloud);
            }

            // Wait for 1 second before spawning the next cloud
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        foreach (var cloud in activeClouds)
        {
            // Move the cloud to the right
            cloud.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
        }

        if (activeClouds.Count >= 10)
        {
            foreach (var cloud in activeClouds.ToList())
            {
                // Check if the cloud is beyond the screen width 
                if (cloud.transform.position.x > Screen.width)
                {
                    // Destroy the cloud
                    Destroy(cloud);
                    activeClouds.Remove(cloud);
                }
            }
        }
    }
}
