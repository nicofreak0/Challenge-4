using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    //variable to control how fast the focal point rotates based on input
    public float speed = 200;
    //variable to reference the player object, which the focal point follows
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //get the horizontal input (e.g., A/D or left/right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        //rotate the focal point around the y-axis (up direction) based on the horizontal input and speed
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
        //update the position of the focal point to match the player's position, making it follow the player
        transform.position = player.transform.position; 

    }
}
