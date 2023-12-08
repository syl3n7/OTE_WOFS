using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab1;
    [SerializeField] private GameObject cloudPrefab2;
    [SerializeField] private GameObject cloudPrefab3;
    [SerializeField] private GameObject cloudPrefab4;
    [SerializeField] private GameObject cloudPrefab5;
    [SerializeField] private GameObject cloudPrefab6;

    [SerializeField] private GameObject cloudPrefab7;
    private float cloudSpeed;

    void OnAwake()
    {
        //get the prefabs from 1 to 7
        cloudPrefab2 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (2)");
        cloudPrefab1 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (1)");
        cloudPrefab3 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (3)");
        cloudPrefab4 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (4)");
        cloudPrefab5 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (5)");
        cloudPrefab6 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (6)");
        cloudPrefab7 = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (7)");
    }

    // Update is called once per frame
    void Update()
    {
        //if currentscene is "In-Game" then generate clouds
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "In-Game")
        {
            //generate clouds
            GenerateClouds();
            //make the clouds move horrizontally on the screen
            foreach (GameObject cloud in clouds)
            {
                cloud.transform.Translate(Vector2.right * cloudSpeed * Time.deltaTime);
            }
        }
    }

    private List<GameObject> clouds = new List<GameObject>(100);

    private void GenerateClouds()
    {
        // Destroy clouds that are out of the player's vision
        for (int i = clouds.Count - 1; i >= 0; i--)
        {
            if (clouds[i].transform.position.y > 10) // Assuming y > 10 is out of player's vision
            {
                Destroy(clouds[i]);
                clouds.RemoveAt(i);
            }
        }

        // Generate new clouds if there are less than 10
        while (clouds.Count < 10)
        {
            //generate clouds randomly on the screen, varying in prefab from 1 to 7 
            int cloudPrefabNumber = Random.Range(1, 8);

            GameObject cloudPrefab = null;

            switch (cloudPrefabNumber)
            {
                case 1:
                    cloudPrefab = cloudPrefab1;
                    break;
                case 2:
                    cloudPrefab = cloudPrefab2;
                    break;
                case 3:
                    cloudPrefab = cloudPrefab3;
                    break;
                case 4:
                    cloudPrefab = cloudPrefab4;
                    break;
                case 5:
                    cloudPrefab = cloudPrefab5;
                    break;
                case 6:
                    cloudPrefab = cloudPrefab6;
                    break;
                case 7:
                    cloudPrefab = cloudPrefab7;
                    break;
            }


            if (cloudPrefab != null)
            {
                // Instantiate the cloud prefab at a random position and with no rotation
                GameObject cloud = Instantiate(cloudPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                cloud.transform.SetParent(gameObject.transform);

                // Set the cloud's speed
                cloudSpeed = Random.Range(240, 340);

                // Add the new cloud to the list
                clouds.Add(cloud);

                // Destroy the cloud after 10 seconds
                Destroy(cloud, 30f);
            }
        }
    }
}
