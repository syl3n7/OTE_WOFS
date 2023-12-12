using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class backgroundSea : MonoBehaviour
{

    //generate Sea "blocks" and make so that appears to the player that the sea is moving
    //! the game needs 3 blocks of the sea preset, one spawning, one in the current view and one being de-spawned.
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
            // Only spawn a new background if there current background is out sight
            if (activeBackground.Count < 3)
            {
                // Instantiate the background at a fixed X and Y position
                GameObject newBackground = Instantiate(backgroundPrefab, new Vector3(Screen.width / 2, Screen.height + 270, 0), Quaternion.identity);

                // set the scale of newBackground to 1.73913, 1.73913, 1.73913
                newBackground.transform.localScale = new Vector3(1.73913f, 1.73913f, 1.73913f);

                //! something here is missing to make it so the background is actually visible and moves apart from one another.

                // Set the newBackgrounds's parent to the Background game object
                newBackground.transform.SetParent(GameObject.Find("Background").transform);

                // Add the cloud to the activeBackground list
                activeBackground.Add(newBackground);
            }
            // Wait for 1 second before spawning the next background
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        foreach (var background in activeBackground)
        {
            // Move the cloud to the right
            background.transform.Translate(Vector2.down * backgroundSpeed * Time.deltaTime);
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
