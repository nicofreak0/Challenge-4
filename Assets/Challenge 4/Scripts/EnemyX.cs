using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    //variable to control the speed of the enemy
    public float speed;
    //variable to store the Rigidbody component of the enemy
    private Rigidbody enemyRb;
    //variable to store the reference to the player's goal in the game
    private GameObject playerGoal;
    //variable to reference the SpawnManagerX script
    private SpawnManagerX spawnManagerX;

    // Start is called before the first frame update
    void Start()
    {
        //get and store the Rigidbody component attached to the enemy object
        enemyRb = GetComponent<Rigidbody>();
        //find and store the reference to the player goal object in the scene
        playerGoal = GameObject.Find("Player Goal");
        //find and store the reference to the SpawnManagerX script for access to its properties
        spawnManagerX = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        //set the enemy's speed using the value from the SpawnManagerX script
        speed = spawnManagerX.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        //apply force to the enemy's Rigidbody to move it towards the player's goal at the specified speed
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        }
        //check if the enemy collides with the "Player Goal" and destroy it
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
