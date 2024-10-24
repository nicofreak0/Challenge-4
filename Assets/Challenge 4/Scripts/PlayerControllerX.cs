using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //variable to store the Rigidbody component for controlling physics-related movement
    private Rigidbody playerRb;
    //variable to control the player's movement speed
    public float speed = 500;
    //variable to store the reference to the "Focal Point" object (used for directional movement)
    private GameObject focalPoint;

    //variable to track if the player has a powerup active
    public bool hasPowerup;
    //variable to reference the GameObject that indicates when the powerup is active
    public GameObject powerupIndicator;
    //variable to set the duration of the powerup in seconds
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    //variable to control the turbo boost speed when space is pressed
    private float turboBoost = 10f;

    //variable to store reference to the particle system for turbo smoke effect
    public ParticleSystem turboSmoke;
    
    void Start()
    {
        //get and store the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();
        //find and store the reference to the focal point (used for directional movement)
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        //apply force to the player in the direction the camera is facing, based on the input
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        //if the space bar is pressed, apply a turbo boost and activate the turbo smoke effect
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
        playerRb.AddForce(focalPoint.transform.forward * turboBoost, ForceMode.Impulse);
            //play turbo smoke particle effect
            turboSmoke.Play();
        }

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        //if the player collides with an object tagged as "Powerup"
        if (other.gameObject.CompareTag("Powerup"))
        {
            //start the powerup duration countdown coroutine
            StartCoroutine(PowerupCooldown());
            //destroy the powerup object after collecting it
            Destroy(other.gameObject);
            //activate powerup state and enable the powerup indicator
            hasPowerup = true;
            powerupIndicator.SetActive(true);
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        //wait for the specified powerup duration
        yield return new WaitForSeconds(powerUpDuration);
        //disable powerup state and hide the powerup indicator
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        //if the player collides with an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            //get the enemy's Rigidbody component for applying force
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //calculate the direction away from the player
            Vector3 awayFromPlayer =   other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
