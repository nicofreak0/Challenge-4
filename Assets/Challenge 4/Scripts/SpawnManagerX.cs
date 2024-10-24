using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    //variables for spawning enemy and powerup prefabs
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    //variables to define the X and Z spawn ranges for enemies and powerups
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    //variables to keep track of the number of enemies and current wave number
    public int enemyCount;
    public int waveCount = 1;
    //variable to control enemy speed, which increases each wave
    public float enemySpeed = 35;

    //variable to reference the player object
    public GameObject player; 

    // Update is called once per frame
    void Update()
    {
        //get the current number of enemies by counting objects tagged as "Enemy"
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //if no enemies remain, spawn a new wave of enemies
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        //random X position within spawn range
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        //random Z position within spawn range
        float zPos = Random.Range(spawnZMin, spawnZMax);
        //return the generated position (Y is fixed at 0)
        return new Vector3(xPos, 0, zPos);
    }


    //method to spawn a wave of enemies and a powerup
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        //increase the wave count and enemy speed for the next wave
        waveCount++;
        ResetPlayerPosition(); // put player back at start
        //increase the speed of the enemies with each wave
        enemySpeed += 25;

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        //set the player's position back to the starting location in front of their goal
        player.transform.position = new Vector3(0, 1, -7);
        //reset the player's velocity and angular velocity to stop movement and rotation
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
