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
        }
    }

    private void GenerateClouds()
    {
        //generate clouds randomly on the screen, varying in prefab from 1 to 7 and in speed from 4 to 9
        int cloudPrefab = Random.Range(1, 7);
        float cloudSpeed = Random.Range(4, 9);
        //generate the clouds
        switch (cloudPrefab)
        {
            case 1:
                Instantiate(cloudPrefab1, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 2:
                Instantiate(cloudPrefab2, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 3:
                Instantiate(cloudPrefab3, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 4:
                Instantiate(cloudPrefab4, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 5:
                Instantiate(cloudPrefab5, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 6:
                Instantiate(cloudPrefab6, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
            case 7:
                Instantiate(cloudPrefab7, new Vector3(10, Random.Range(-3, 3), 0), Quaternion.identity).GetComponent<CloudGeneration>().cloudSpeed = cloudSpeed;
                break;
        }
    }
}
