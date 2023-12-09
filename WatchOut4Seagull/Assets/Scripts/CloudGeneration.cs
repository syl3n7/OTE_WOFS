using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudGeneration : MonoBehaviour
{
    private float cloudSpeed;

    GameObject[] cloudPrefab = new GameObject[7];

    void OnAwake()
    {
        //get the prefabs from 1 to 7
        cloudPrefab[0] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (1)");
        cloudPrefab[1] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (2)");
        cloudPrefab[2] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (3)");
        cloudPrefab[3] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (4)");
        cloudPrefab[4] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (5)");
        cloudPrefab[5] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (6)");
        cloudPrefab[6] = Resources.Load<GameObject>("Prefabs/Clouds/Cloud (7)");
    }

    void Update()
    {
        //generate a random number between 0 and 6
        int randomCloud = Random.Range(0, 7);

        //instantiate the cloud prefab
        GameObject cloud = Instantiate(cloudPrefab[randomCloud]);

        foreach (var cloud in cloudPrefab)
        {
            //set the cloud's speed to a random number between 0.5 and 1.5
            cloudSpeed = Random.Range(150, 250);

            //move the cloud to the left
            cloud.transform.Translate(Vector3.left * cloudSpeed * Time.deltaTime);
        }

        //set the cloud's scale to 0.5
        cloud.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //set the cloud's position to a random position between -10 and 10 on the x axis, 5 on the y axis, and 0 on the z axis
        cloud.transform.position = new Vector3(Random.Range(-10, 10), 5, 0);
    }

}
